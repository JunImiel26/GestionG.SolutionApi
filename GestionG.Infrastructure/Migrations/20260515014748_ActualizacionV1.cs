using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_AspNetUsers_IdUsuario",
                table: "Detalles");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_IdUsuario",
                table: "Detalles");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Detalles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Detalles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_IdUsuario",
                table: "Detalles",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_AspNetUsers_IdUsuario",
                table: "Detalles",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
