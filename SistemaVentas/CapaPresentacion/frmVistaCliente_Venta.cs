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
    public partial class frmVistaCliente_Venta : Form
    {
        public frmVistaCliente_Venta()
        {
            InitializeComponent();
        }

        private void frmVistaCliente_Venta_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }
        //Metodopara ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        //Metodo mostrar los registros de la tabala categoria
        private void Mostrar()
        {
            this.dataListado.DataSource = NCliente.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo BuscarNombre
        private void BuscarApellidos()
        {
            this.dataListado.DataSource = NCliente.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void BuscarNumDocumento()
        {
            this.dataListado.DataSource = NCliente.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
            else if (cboBuscar.Text.Equals("Documento"))
            {
                this.BuscarNumDocumento();
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {

            frmVenta fr = frmVenta.Getinstancia();
            string par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idcliente"].Value);
            string par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["apellidos"].Value)+" "+
                          Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            fr.setCliente(par1,par2);
            this.Hide();


        }
    }
}
