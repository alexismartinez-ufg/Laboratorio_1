using Laboratorio_1.Models;
using Laboratorio_1.Service;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Laboratorio_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSeeding();
            ConfigurarListView();
        }

        /// <summary>
        /// Termina la compra y cierra la aplicación
        /// </summary>
        public void FinishCompra()
        {
            int totalArticulos = 0;
            decimal descuentoTotal = 0m;
            decimal totalAPagar = 0m;

            foreach (var compra in ComprasService.Compras)
            {
                totalArticulos += compra.Cantidad;
                descuentoTotal += compra.Descuento;
                totalAPagar += compra.Total;
            }

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine("¡Gracias por tu compra!");
            mensaje.AppendLine($"Total de artículos: {totalArticulos}");
            mensaje.AppendLine($"Descuento total: {descuentoTotal.ToString("C")}");
            mensaje.AppendLine($"Total a pagar: {totalAPagar.ToString("C")}");

            MessageBox.Show(mensaje.ToString(), "Resumen de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Application.Exit();
        }

        /// <summary>
        /// limpia los inputs y textos mostrados en el grup de compras
        /// </summary>
        public void CleanCompras()
        {
            cbAtributos.SelectedIndex = -1;
            txbCantidad.Value = 1;
            this.lblPrecioUnitario.Text = Articulos.ZeroPrice;
            this.lblSubTotal.Text = Articulos.ZeroPrice;
        }

        /// <summary>
        /// Configura mi listview para que las columnas se muestren en el 
        /// espacio determinado
        /// </summary>
        private void ConfigurarListView()
        {
            listComprados.View = View.Details;

            listComprados.Columns.Add("Nombre del Producto", 120);
            listComprados.Columns.Add("Precio Unitario", 105);
            listComprados.Columns.Add("Subtotal", 70);
            listComprados.Columns.Add("Descuento", 70);
            listComprados.Columns.Add("Porcentaje Descuento", 136);
            listComprados.Columns.Add("Total", 75);

            listComprados.Scrollable = true;
        }

        /// <summary>
        /// Agrega compras en la lista statica de compras
        /// </summary>
        public void AgregarCompra()
        {
            if (this.cbAtributos.SelectedItem == null)
                return;
            if (string.IsNullOrEmpty(lblPrecioUnitario.Text))
                return;
            if (txbCantidad.Value < 1)
                return;

            var compra = new Compra(this.cbAtributos.SelectedItem.ToString(), decimal.Parse(lblPrecioUnitario.Text.Replace("$", "")), (int)txbCantidad.Value);

            ComprasService.Compras.Add(compra);
        }

        /// <summary>
        /// Actualiza el listView con las compras actualizadas
        /// </summary>
        public void ActualizarListView()
        {
            listComprados.Items.Clear();
            foreach (var compra in ComprasService.Compras)
            {
                var item = new ListViewItem(new[]
                {
                     compra.NombreProducto,
                     compra.PrecioUnitario.ToString("C"),
                     compra.Subtotal.ToString("C"),
                     compra.Descuento.ToString("C"),
                     compra.Porcentaje.ToString()+"%",
                     compra.Total.ToString("C")
                 });

                listComprados.Items.Add(item);
            }
        }

        /// <summary>
        /// Calcula el descuento con base en las compras realizadas
        /// </summary>
        public void CalculateDescuentoTotal()
        {
            decimal descuentoTotal = 0;
            decimal totalAPagar = 0;

            foreach (var compra in ComprasService.Compras)
            {
                descuentoTotal += compra.Descuento;
                totalAPagar += compra.Total;
            }

            lblDescuentoTotal.Text = descuentoTotal.ToString("C");
            lblTotal.Text = totalAPagar.ToString("C");
        }

        /// <summary>
        /// Calcula el subtotal de cada compra
        /// </summary>
        public void CalculateSubTotal()
        {
            this.lblSubTotal.Text = Articulos.ZeroPrice;

            if (string.IsNullOrEmpty(lblPrecioUnitario.Text))
                return;
            if (txbCantidad.Value < 1)
                return;

            this.lblSubTotal.Text = (decimal.Parse(lblPrecioUnitario.Text.Replace("$","")) * txbCantidad.Value).ToString("C");
        }

        /// <summary>
        /// Carga los datos seed o base en el programa cuando este carga
        /// </summary>
        public void LoadSeeding()
        {
            this.cbAtributos.Items.AddRange(ArticloService.listaArticulos.Select(x=>x.NombreArticulo).ToArray());
            this.lblDescuentoTotal.Text = Articulos.ZeroPrice;
            this.lblPrecioUnitario.Text = Articulos.ZeroPrice;
            this.lblSubTotal.Text = Articulos.ZeroPrice;
            this.lblTotal.Text = Articulos.ZeroPrice;
        }

        /// <summary>
        /// Se muestra el precio con base en el articulo seleccionado
        /// </summary>
        public void ShowPriceByArtc()
        {
            if (this.cbAtributos.SelectedItem == null)
                return;

            var currentArtTxt = this.cbAtributos.SelectedItem.ToString();

            var currentArtPrice = ArticloService.listaArticulos.FirstOrDefault(a => a.NombreArticulo == currentArtTxt);

            if (currentArtPrice != null)
                this.lblPrecioUnitario.Text = currentArtPrice.Precio.ToString("C");
            else
                this.lblPrecioUnitario.Text = Articulos.ZeroPrice;
        }

        /// <summary>
        /// EVENTOS EJECUTADOS POR LOS ELEMENTOS DE LA UI
        /// </summary>

        #region Eventos de la UI
        private void cbAtributos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShowPriceByArtc();
            CalculateSubTotal();
        }

        private void txbCantidad_ValueChanged(object sender, System.EventArgs e)
        {
            CalculateSubTotal();
        }

        private void btnAgregarProducto_Click(object sender, System.EventArgs e)
        {
            AgregarCompra();
            ActualizarListView();
            CalculateDescuentoTotal();
            CleanCompras();
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnComprar_Click(object sender, System.EventArgs e)
        {
            FinishCompra();
        }
        #endregion
    }
}
