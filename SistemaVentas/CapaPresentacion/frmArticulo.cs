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
    public partial class frmArticulo : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        private static frmArticulo _Instancia; //Hace una instancia al frmArticulo

        //MEtodo que crea una isntancia 
        public static frmArticulo GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmArticulo();
            }
            return _Instancia;
        }

        //Metodo para enviar los valores recibidos a las caja de texto idcategoria
        public void AsignarCategoria(string idcategoria, string NombreCategoria)
        {
            this.txtIdCategoria.Text = idcategoria;
            this.txtCategoria.Text = NombreCategoria;
        }


        public frmArticulo()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el Nombre del Articulo");
            this.ttMensaje.SetToolTip(this.pxImagen, "Seleccione la imagen del articulo");
            this.ttMensaje.SetToolTip(this.txtCategoria, "Ingrese el Nombre del Articulo");
            this.ttMensaje.SetToolTip(this.cbIdpresentacion, "Seleccione la presentacion del articulo");


            this.txtIdCategoria.Visible = false; //PROBAR READONLY
            this.txtCategoria.ReadOnly = true;

            LLenarComboPresentacion();

        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //mostrar un mensaje de error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Metodo para limpiar todos los controles del frm
        private void LimpiarControles()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.txtIdarticulo.Text = string.Empty;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.sin_imagen;//Esta imagen se enviara a la base de datos cuando no se registre imagen

        }

        //Habilitar los controles dle form
        private void HabilitarCajasTexto(bool valor)
        {
            this.txtCodigo.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.cbIdpresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnLimpiar.Enabled = valor;
            this.txtIdarticulo.ReadOnly = !valor;
        }

        //Metdo habilitar los botones
        private void BotonesHabilitados()
        {
            if (this.IsNuevo || this.IsEditar)
            {
                HabilitarCajasTexto(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                HabilitarCajasTexto(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;

            }
        }

        //Metodopara ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;//Eliminar que agregamos en el form
            this.dataListado.Columns[1].Visible = false;//campo idpresentacion 
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;
        }

        //Metodo mostrar los registros de la tabala categoria
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        //MEtodo que llene las presentaciones en el combobox
        private void LLenarComboPresentacion()
        {
            cbIdpresentacion.DataSource = NPresentacion.Mostrar();//el metodo DataSource permite almacenar datos
            cbIdpresentacion.ValueMember = "idpresentacion"; //se obtiene el id
            cbIdpresentacion.DisplayMember = "nombre";//se muestra en el cb los nombres
        }



        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.HabilitarCajasTexto(false);
            this.BotonesHabilitados();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();//mostramos el cuadro de cargar imagenes

            DialogResult result = dialog.ShowDialog();//El resultado es lo que el usuario selecciones

            if (result==DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);//obtenemos el nombre del archivo desde el dialog
            }


        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.sin_imagen;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            this.IsNuevo = true;
            this.IsEditar = false;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(true);
            this.txtNombre.Focus();
            this.txtIdarticulo.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtIdCategoria.Text == string.Empty || this.txtCodigo.Text == string.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un valor");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(txtCategoria, "Ingrese un valor");
                    errorIcono.SetError(txtCodigo, "Ingrese un valor");


                }
                else
                {
                    //Enviar la imagen
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);

                    byte[] imagen1 = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text,this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(),
                            imagen1,Convert.ToInt32(this.txtIdCategoria.Text),Convert.ToInt32(cbIdpresentacion.SelectedValue));//Trim elimina los espacios en blanco   


                    }
                    else
                    {
                        rpta = NArticulo.Editar(Convert.ToInt32(this.txtIdarticulo.Text.Trim().ToUpper()), this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(),
                            imagen1, Convert.ToInt32(this.txtIdCategoria.Text), Convert.ToInt32(cbIdpresentacion.SelectedValue));
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            MensajeOk("SE INSERTO de fomra correcta el Registro-Articulo");
                        }
                        else
                        {
                            MensajeOk("SE EDITO-ACTUALIZO de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    BotonesHabilitados();
                    LimpiarControles();
                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdarticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

            this.pxImagen.Image = Image.FromStream(ms);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            this.cbIdpresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);




            this.tabControl1.SelectedIndex = 1;//para que automaticamente muestre el siguiente tabpage

        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea realmente eliminar los Registros?", "Sistema de VEntas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows) //Recorremos con un bucle las filas del datalistado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) //SI encontramos alguna fila seleccionada [0] = Eliminar
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value); //Codigo sera igual a la celda[1] que es la llave idcategoria;
                            rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se ELIMINO Correctamente el Registro");
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Message);
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmVistaModalCategoria_Articulo form = new frmVistaModalCategoria_Articulo();
            form.ShowDialog();//mostrarlo de manera emergente
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (!this.txtIdarticulo.Text.Equals(""))
            {
                this.IsEditar = true;
                this.BotonesHabilitados();
                this.HabilitarCajasTexto(true);
            }
            else
            {
                this.MensajeError("DEbe de seleccionar primero el registro a EDITAR :)s");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReporteArticulos fra = new frmReporteArticulos();
            fra.ShowDialog();
        }
    }
}
