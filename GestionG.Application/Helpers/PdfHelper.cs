using GestionG.Application.DTOs.Gasto;
using System.Text;

namespace GestionG.Application.Helpers
{
    public static class PdfHelper
    {
        // Implementación sencilla: devuelve un texto con formato que el cliente puede descargar
        // como archivo .pdf. Para PDF real se puede integrar una librería (QuestPDF, iText, etc.).
        public static byte[] GenerateGastosPdf(IEnumerable<GastoDTo> gastos, decimal total)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Historial de Gastos");
            sb.AppendLine($"Total: {total:C}");
            sb.AppendLine(new string('-', 40));
            foreach (var g in gastos)
            {
                sb.AppendLine($"Gasto {g.IdGasto} - Fecha: {g.Fecha:yyyy-MM-dd} - Total: {g.TotalGeneral:C}");
                if (g.Detalles != null && g.Detalles.Any())
                {
                    foreach (var d in g.Detalles)
                    {
                        sb.AppendLine($"  - {d.Descripcion}: {d.Monto:C} (Categoria ID {d.IdCat})");
                    }
                }
                sb.AppendLine(new string('-', 20));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
