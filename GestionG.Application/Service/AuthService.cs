using AutoMapper;
using GestionG.Application.DTOs.Usuario;
using GestionG.Application.Interface.Repository;
using GestionG.Application.Interface.Service;
using GestionG.Application.Response;
using GestionG.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestionG.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refresRepo;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration config,
            IRefreshTokenRepository refresRepo,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _refresRepo = refresRepo;
            _mapper = mapper;
        }

        #region Métodos Privados

        private async Task<UsuarioDTo> MapearUsuarioDtoAsync(Usuario usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDTo
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre ?? usuario.UserName!,
                Email = usuario.Email!,
                Rol = roles.FirstOrDefault() ?? "Sin Rol",
            };
        }

        private static void ValidarResultado(IdentityResult resultado, string mensajeError)
        {
            if (!resultado.Succeeded)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"{mensajeError}: '{errores}'");
            }
        }

        private string GenerarToken(Usuario usuario, string rol, DateTime expiracion)
        {
            var key = _config["JWT_KEY"] ?? throw new Exception("JWT_KEY no configurado en appsettings.json");
            var issuer = _config["JWT_ISSUER"] ?? "GestionG.Api";
            var audience = _config["JWT_AUDIENCE"] ?? "GestionG.App";

            var keyBytes = Encoding.UTF8.GetBytes(key);

            // 1. Definición de los Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName ?? ""),
                new Claim(ClaimTypes.Email, usuario.Email ?? ""),
                // ✅ Crucial: ClaimTypes.Role permite que [Authorize(Roles="...")] funcione
                new Claim(ClaimTypes.Role, rol)
            };

            // 2. 💡 CORRECCIÓN VITAL: Especificar "JwtBearer" en el ClaimsIdentity
            // Esto le indica a .NET que use los esquemas de nombres estándar para los roles.
            var Subject = new ClaimsIdentity(claims, "Bearer");

            // 3. Creación del Descriptor del Token
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = Subject,
                Expires = expiracion,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Generación final
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        private string GenerarRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        #endregion

        public async Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDTo dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var email = dto.Email.Trim().ToLower();
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) throw new UnauthorizedAccessException("Usuario no registrado.");

            if (!await _userManager.CheckPasswordAsync(usuario, dto.Password))
                throw new UnauthorizedAccessException("Contraseña incorrecta.");

            // Gestión de Roles
            var roles = await _userManager.GetRolesAsync(usuario);
            var rolNombre = roles.FirstOrDefault() ?? "Usuario"; // Valor por defecto si no tiene rol

            var expiracion = DateTime.UtcNow.AddMinutes(60); // Aumentado a 60 min para pruebas

            var jwt = GenerarToken(usuario, rolNombre, expiracion);
            var refresh = GenerarRefreshToken();

            // Persistencia del Refresh Token
            await _refresRepo.GuardarAsync(new RefreshToken
            {
                Token = refresh,
                UsuarioId = usuario.Id,
                Expiracion = DateTime.UtcNow.AddDays(7)
            });

            return new RespuestaLoginDto
            {
                Usuario = await MapearUsuarioDtoAsync(usuario),
                AccessToken = jwt,
                RefreshToken = refresh,
                ExpiraEn = expiracion
            };
        }

        public async Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken)
        {
            var tokenDB = await _refresRepo.ObtenerAsync(refreshToken);
            if (tokenDB == null || tokenDB.Expiracion < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token inválido o expirado.");

            var usuario = tokenDB.Usuario;
            var roles = await _userManager.GetRolesAsync(usuario);
            var rol = roles.FirstOrDefault() ?? "Usuario";

            var expiracion = DateTime.UtcNow.AddMinutes(60);

            var nuevoJwt = GenerarToken(usuario, rol, expiracion);
            var nuevoRefresh = GenerarRefreshToken();

            tokenDB.Token = nuevoRefresh;
            tokenDB.Expiracion = DateTime.UtcNow.AddDays(7);
            await _refresRepo.ActualizarAsync(tokenDB);

            return new RespuestaLoginDto
            {
                Usuario = await MapearUsuarioDtoAsync(usuario),
                AccessToken = nuevoJwt,
                RefreshToken = nuevoRefresh,
                ExpiraEn = expiracion
            };
        }

        public async Task<UsuarioDTo> RegistrarUsuarioAsync(UsuariosRegistroDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            dto.Email = dto.Email.Trim().ToLower();

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                throw new InvalidOperationException("El email ya se encuentra registrado.");

            // Validar que el rol exista antes de intentar asignar
            if (!await _roleManager.RoleExistsAsync(dto.Rol))
                throw new InvalidOperationException($"El rol '{dto.Rol}' no existe en el sistema.");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Email = dto.Email;
            usuario.UserName = dto.Email;
            usuario.Nombre = dto.NombreCompleto; // Asegurar mapeo de nombre
            usuario.EmailConfirmed = true;

            var usuarioCreado = await _userManager.CreateAsync(usuario, dto.Password);
            ValidarResultado(usuarioCreado, "Error al registrar el usuario");

            var rolAsignado = await _userManager.AddToRoleAsync(usuario, dto.Rol);
            ValidarResultado(rolAsignado, "Error al asignar el rol");

            return await MapearUsuarioDtoAsync(usuario);
        }
    }
}