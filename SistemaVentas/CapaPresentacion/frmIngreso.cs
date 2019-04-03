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
    public partial class frmIngreso : Form
    {
        public int idtrabajador;//enviar el id trabajado desde mi id principal
        private static frmIngreso _instancia;

        public static frmIngreso Getinstancia()
        {
            if (_instancia==null)
            {
                _instancia = new frmIngreso();
            }
            return _instancia;
        }

        public void setProveedor(string idProveedor, string nombreProv)//Asignar a las cajas de texto
        {
            this.txtIdProveedor.Text = idProveedor;
            this.txtProveedor.Text = nombreProv;
        }

        public void setArticulo(string idarticulo, string nombre)
        {
            this.txtIdArticulo.Text = idarticulo;
            this.txtArticulo.Text = nombre;
        }


        public frmIngreso()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {

        }



        private void frmIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;//CUando el formulario se cierra la instancia debe de estar null
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            frmVistaProveedor_Ingreso frm2 = new frmVistaProveedor_Ingreso();
            frm2.ShowDialog();
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            frmVistaArticulo_Ingreso fr = new frmVistaArticulo_Ingreso();
            fr.ShowDialog();
        }
    }
}
