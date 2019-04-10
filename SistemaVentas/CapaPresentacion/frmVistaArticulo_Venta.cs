using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmVistaArticulo_Venta : Form
    {
        public frmVistaArticulo_Venta()
        {
            InitializeComponent();
        }

        private void frmVistaArticulo_Venta_Load(object sender, EventArgs e)
        {

        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        //Metodo mostrar los registros de la tabala categoria


        //Metodo BuscarNombre
        private void MostrarArticulo_Venta_Nombre()
        {
            this.dataListado.DataSource = NVenta.MostrarArticulo_Venta_Nombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void MostrarArticulo_Venta_Codigo()
        {
            this.dataListado.DataSource = NVenta.MostrarArticulo_Venta_Codigo(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboBuscar.Text.Equals("Codigo"))
            {
                this.MostrarArticulo_Venta_Codigo();
            }
            else if (cboBuscar.Text.Equals("Nombre"))
            {
                this.MostrarArticulo_Venta_Nombre();
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmVenta frmv = frmVenta.Getinstancia();
            string par1, par2;
            decimal par3, par4;
            int par5;
            DateTime par6;
            par1=Convert.ToString(dataListado.CurrentRow.Cells["iddetalle_ingreso"].Value);
            par2 = Convert.ToString(dataListado.CurrentRow.Cells["nombre"].Value);
            par3 = Convert.ToDecimal(dataListado.CurrentRow.Cells["precio_compra"].Value);
            par4 = Convert.ToDecimal(dataListado.CurrentRow.Cells["precio_venta"].Value);
            par5 = Convert.ToInt32(dataListado.CurrentRow.Cells["stock_actual"].Value);
            par6 = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha_vencimiento"].Value);

            frmv.setArticulo(par1,par2,par3,par4,par5,par6);
            this.Hide();


        }
    }
}
