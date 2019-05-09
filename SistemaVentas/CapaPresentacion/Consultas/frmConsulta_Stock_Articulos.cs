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

namespace CapaPresentacion.Consultas
{
    public partial class frmConsulta_Stock_Articulos : Form
    {
        public frmConsulta_Stock_Articulos()
        {
            InitializeComponent();
        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;//Eliminar que agregamos en el form
           
        }

        //Metodo mostrar los registros de la tabala categoria
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Stock();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ttMensaje_Popup(object sender, PopupEventArgs e)
        {

        }

        private void frmConsulta_Stock_Articulos_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }
    }
}
