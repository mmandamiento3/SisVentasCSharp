using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmVenta : Form
    {
        private bool IsNuevo=false;
        public int IdTrbajador;
        private DataTable dtDetalle;//Los detalles d ela venta
        private decimal TotalPagado = 0;


        //metodo que verifica la instancia
        private static frmVenta _Instancia;
        public static frmVenta Getinstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmVenta();
            }
            return _Instancia;
        }

        public void setCliente(string idcliente, string nombre)
        {
            this.txtIdCLiente.Text = idcliente;
            this.txtCliente.Text = nombre;
        }

        public void setArticulo(string iddetalle_ingreso,string nombre, 
            decimal precio_compra, decimal precio_venta, int stock,
            DateTime fecha_vencimiento)
        {
            this.txtIdArticulo.Text = iddetalle_ingreso;
            this.txtArticulo.Text = nombre;
            this.txtPrecioCompra.Text = Convert.ToString(precio_compra);
            this.txtPrecioVenta.Text = Convert.ToString(precio_venta);
            this.txtStockActual.Text = Convert.ToString(stock);
            this.dtFechaVencimiento.Value = fecha_vencimiento;

        }



        public frmVenta()
        {
            InitializeComponent();
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {

        }

        private void frmVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }
    }
}
