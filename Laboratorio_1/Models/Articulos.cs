namespace Laboratorio_1.Models
{
    /// <summary>
    /// Representa los articulos disponibles
    /// </summary>
    public class Articulos
    {
        public string NombreArticulo { get; set; }
        public double Precio { get; set; }

        public static string ZeroPrice = "$0.00";
    }
}
