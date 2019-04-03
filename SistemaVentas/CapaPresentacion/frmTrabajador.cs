﻿using CapaNegocio;
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
    public partial class frmTrabajador : Form
    {

        private bool IsNuevo = false;
        private bool IsEditar = false;

        public frmTrabajador()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre,"Ingrese el Nombre del Trabajador");
            this.ttMensaje.SetToolTip(this.txtApellidos, "Ingrese el Apellido del Trabajador");
            this.ttMensaje.SetToolTip(this.txtUsuario, "Ingrese el Usuario para que el Trabajador ingrese al Sistema");
            this.ttMensaje.SetToolTip(this.txtPassword, "Ingrese el Password del Trabajador");
            this.ttMensaje.SetToolTip(this.cboAcceso, "Seleccione el nivel de Acceso del Trabajador");


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
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtNumdocumento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtUsuario.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
            this.txtIdTrabajado.Text = string.Empty;




        }

        //Habilitar los controles dle form
        private void HabilitarCajasTexto(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.cboSexo.Enabled = valor;
            this.txtNumdocumento.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.cboAcceso.Enabled = valor;
            this.txtUsuario.ReadOnly = !valor;
            this.txtPassword.ReadOnly = !valor;
            this.txtIdTrabajado.ReadOnly = !valor;
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
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        //Metodo mostrar los registros de la tabala categoria
        private void Mostrar()
        {
            this.dataListado.DataSource = NTrabajador.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo BuscarNombre
        private void BuscarApellidos()
        {
            this.dataListado.DataSource = NTrabajador.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void BuscarNumDocumento()
        {
            this.dataListado.DataSource = NTrabajador.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void frmTrabajador_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.HabilitarCajasTexto(false);
            this.BotonesHabilitados();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboBuscar.Text.Equals("Documento"))
            {
                this.BuscarNumDocumento();
            }
            else if (cboBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
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
                            rpta = NTrabajador.Eliminar(Convert.ToInt32(Codigo));

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

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible=true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminarr = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminarr.Value = !Convert.ToBoolean(ChkEliminarr.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdTrabajado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idtrabajador"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtApellidos.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["apellidos"].Value);
            this.cboSexo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["sexo"].Value);
            this.dtFechaNac.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fecha_nac"].Value);
            this.txtNumdocumento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["num_documento"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["telefono"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["email"].Value);
            this.cboAcceso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["acceso"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["usuario"].Value);
            this.txtPassword.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["password"].Value);


            this.tabControl1.SelectedIndex = 1;//para que automaticamente muestre el siguiente tabpage

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(true);
            this.txtNombre.Focus();
            this.txtIdTrabajado.Enabled = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(false);
            this.txtIdTrabajado.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtApellidos.Text == string.Empty ||
                  this.txtNumdocumento.Text == string.Empty
                    || this.txtDireccion.Text == string.Empty||this.txtUsuario.Text==string.Empty
                    ||this.txtPassword.Text==String.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos remarcados");
                    errorIcono.SetError(this.txtNombre, "Ingrese un Nombre");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(this.txtApellidos, "Ingrese los Apellidos");
                    errorIcono.SetError(this.txtNumdocumento, "Ingrese el numero de documento");
                    errorIcono.SetError(this.txtDireccion, "Ingrese una Direccion");
                    errorIcono.SetError(this.txtUsuario, "Ingrese un Usuario");
                    errorIcono.SetError(this.txtPassword, "Ingrese una contraseña");


                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NTrabajador.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtApellidos.Text.Trim().ToUpper(), this.cboSexo.Text,
                            this.dtFechaNac.Value, this.txtNumdocumento.Text, this.txtDireccion.Text, this.txtTelefono.Text,
                            txtEmail.Text,this.cboAcceso.Text, this.txtUsuario.Text,this.txtPassword.Text);//Trim elimina los espacios en blanco   


                    }
                    else
                    {
                        rpta = NTrabajador.Editar(Convert.ToInt32(this.txtIdTrabajado.Text.Trim().ToUpper()), this.txtNombre.Text.Trim().ToUpper(), this.txtApellidos.Text.Trim().ToUpper(), this.cboSexo.Text,
                            this.dtFechaNac.Value, this.txtNumdocumento.Text, this.txtDireccion.Text, this.txtTelefono.Text,
                            txtEmail.Text, this.cboAcceso.Text, this.txtUsuario.Text, this.txtPassword.Text);

                    }
                    if (rpta.Equals("OK"))
                    { 
                        if (this.IsNuevo)
                        {
                            MensajeOk("SE INSERTO de fomra correcta el registro");
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
            if (!this.txtIdTrabajado.Text.Equals(""))
            {
                this.IsEditar = true;
                this.BotonesHabilitados();
                this.HabilitarCajasTexto(true);
                txtIdTrabajado.Enabled = false;
            }
            else
            {
                this.MensajeError("DEbe de seleccionar rimero el registro a EDITAR :)s");
            }
        }
    }
}
