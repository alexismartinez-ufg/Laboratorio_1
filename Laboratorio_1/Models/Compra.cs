namespace Laboratorio_1.Models
{
    /// <summary>
    /// Representación de las compras que se realizaran en el sistema
    /// </summary>
    public class Compra
    {
        public string NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; private set; }
        public decimal Descuento { get; private set; }
        public decimal Porcentaje { get; private set; }
        public decimal Total => Subtotal - Descuento;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nombreProducto">Nombre del producto comprado</param>
        /// <param name="precioUnitario">precio de la unidad con decimales</param>
        /// <param name="cantidad">numero de entidades del mismo tipo a comprar</param>
        public Compra(string nombreProducto, decimal precioUnitario, int cantidad)
        {
            NombreProducto = nombreProducto;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
            CalcularSubtotal();
            CalcularDescuento();
        }

        private void CalcularSubtotal()
        {
            Subtotal = PrecioUnitario * Cantidad;
        }

        /// <summary>
        /// lógica de descuentos a aplicar a las compras
        /// </summary>
        private void CalcularDescuento()
        {
            if (Subtotal >= 500)
            {
                Porcentaje = 0.30m;
                Descuento = Subtotal * 0.30m;
            }
            else if (Subtotal >= 300)
            {
                Porcentaje = 0.25m;
                Descuento = Subtotal * 0.25m;
            }
            else if (Subtotal >= 100)
            {
                Porcentaje = 0.15m;
                Descuento = Subtotal * 0.15m;
            }
            else
            {
                Porcentaje = 0;
                Descuento = 0;
            }
        }
    }

}
