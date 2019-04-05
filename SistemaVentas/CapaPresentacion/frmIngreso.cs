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
    public partial class frmIngreso : Form
    {
        public int idtrabajador;//enviar el id trabajado desde mi id principal
        
        private static frmIngreso _instancia;

        private bool IsNuevo;
        private DataTable dtDetalle;//alamacenar los detalles d elos ingresos
        private decimal totalPagado = 0;

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
            this.ttMensaje.SetToolTip(this.txtProveedor,"Seleccione el Proveedor");
            this.ttMensaje.SetToolTip(this.txtSerie, "Ingrese el Comprobante");
            this.ttMensaje.SetToolTip(this.txtCoreelativo, "Ingrese el número del Comprobante");
            this.ttMensaje.SetToolTip(this.txtStock, "Ingrese la cantidad de Compra");
            this.ttMensaje.SetToolTip(this.txtArticulo, "Seleccione el Articulo de compra");
            this.txtIdProveedor.Visible = false;
            this.txtIdArticulo.Visible = false;
            this.txtProveedor.ReadOnly = true;
            this.txtArticulo.ReadOnly = true;
         

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
            this.txtIdIngreso.Text = string.Empty;
            this.txtIdProveedor.Text = string.Empty;
            this.txtProveedor.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCoreelativo.Text = string.Empty;
            this.txtIGV.Text = string.Empty;
            this.lblTotalPagado.Text = "0.0";
            this.txtIGV.Text = "18";
            this.CrearTabla();
        }



        private void limpiarDetalleIngreso()
        {
            this.txtIdArticulo.Text = string.Empty;
            this.txtArticulo.Text = string.Empty;
            this.txtStock.Text = string.Empty;
            this.txtPrecioCompra.Text = string.Empty;
            this.txtPrecioVenta.Text = string.Empty;

        }

        //Habilitar los controles dle form
        private void HabilitarCajasTexto(bool valor)
        {
            this.txtIdIngreso.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCoreelativo.ReadOnly = !valor;
            this.txtIGV.ReadOnly = !valor;
            this.dtFecha.Enabled = valor;
            this.cboTipoComprobante.Enabled = valor;
            this.txtStock.ReadOnly = !valor;
            this.txtPrecioCompra.ReadOnly = !valor;
            this.txtPrecioVenta.ReadOnly = !valor;
            this.dtFechaProduccion.Enabled = valor;
            this.dtFechaVencimiento.Enabled = valor;

            this.btnBuscarArticulo.Enabled = valor;
            this.btnBuscarProveedor.Enabled = valor;
            this.btnAgregar.Enabled = valor;
            this.btnQuitar.Enabled = valor;
           
        }

        //Metdo habilitar los botones
        private void BotonesHabilitados()
        {
            if (this.IsNuevo)
            {
                HabilitarCajasTexto(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;                
                this.btnCancelar.Enabled = true;
            }
            else
            {
                HabilitarCajasTexto(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;                
                this.btnCancelar.Enabled = false;

            }
        }

        //Metodopara ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;//Eliminar que agregamos en el form
            this.dataListado.Columns[1].Visible = false;//campo idpresentacion 
     
        }

        //Metodo mostrar los registros de la tabala categoria
        private void Mostrar()
        {
            this.dataListado.DataSource = NIngreso.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo BuscarNombre
        private void BuscarFechas()
        {
            this.dataListado.DataSource = NIngreso.BuscarFEchas(this.dtFecha1.Value.ToString("dd/MM/yyyy"), this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void MostrarDetalles()
        {
            this.dataListadoDetalles.DataSource = NIngreso.MostrarDetalle(this.txtIdIngreso.Text);
           
        }

        public void CrearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("idarticulo",System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("articulo",System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("precio_compra",System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("precio_venta",System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("stock_inicial",System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("fecha_produccion",System.Type.GetType("System.DateTime"));
            this.dtDetalle.Columns.Add("fecha_vencimiento",System.Type.GetType("System.DateTime"));
            this.dtDetalle.Columns.Add("subtotal",System.Type.GetType("System.Decimal"));

            //Relacionando nuestro datagridview con nuestra datatable
            this.dataListadoDetalles.DataSource = this.dtDetalle;

        }




        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.BotonesHabilitados();
            this.HabilitarCajasTexto(false);
            this.CrearTabla();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarFechas();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea realmente anular los Registros?", "Sistema de VEntas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows) //Recorremos con un bucle las filas del datalistado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) //SI encontramos alguna fila seleccionada [0] = Eliminar
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value); //Codigo sera igual a la celda[1] que es la llave idcategoria;
                            rpta = NIngreso.Anular(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se ANULOOOOO Correctamente el Registro");
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
            if (this.chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
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
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            this.IsNuevo = true;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(true);
            this.txtSerie.Focus();
            this.limpiarDetalleIngreso();
            this.txtIdIngreso.Enabled = false;
          
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;          
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.HabilitarCajasTexto(false);
            this.limpiarDetalleIngreso();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtIdProveedor.Text == string.Empty || this.txtSerie.Text == string.Empty 
                    || this.txtCoreelativo.Text == string.Empty || this.txtIGV.Text == string.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos remarcados");
                    errorIcono.SetError(txtIdProveedor, "Ingrese un valor");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(txtSerie, "Ingrese un valor");
                    errorIcono.SetError(txtCoreelativo, "Ingrese un valor");
                    errorIcono.SetError(txtIGV, "Ingrese un valor");


                }
                else
                {
                    //Enviar la imagen
                   
                    if (this.IsNuevo)
                    {
                        rpta = NIngreso.Insertar(idtrabajador,Convert.ToInt32(this.txtIdProveedor.Text),
                           dtFecha.Value,cboTipoComprobante.Text,txtSerie.Text,txtCoreelativo.Text,
                           Convert.ToDecimal(txtIGV.Text),"Emitido",dtDetalle);//Trim elimina los espacios en blanco   


                    }
                   
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            MensajeOk("SE INSERTO de fomra correcta el Registro-Articulo");
                        }
                       
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                 
                    BotonesHabilitados();
                    LimpiarControles();
                    limpiarDetalleIngreso();
                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.txtIdArticulo.Text == string.Empty || this.txtStock.Text == string.Empty || this.txtPrecioCompra.Text == string.Empty
                    || this.txtPrecioVenta.Text == string.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos remarcados");
                    errorIcono.SetError(txtIdArticulo, "Ingrese un valor");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(txtStock, "Ingrese un valor");
                    errorIcono.SetError(txtPrecioCompra, "Ingrese un valor");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese un valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idarticulo"])==Convert.ToInt32(this.txtIdArticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el articulo en el detalle");
                        }
                    }

                    if (registrar)
                    {
                        decimal subtotal = Convert.ToDecimal(this.txtStock.Text) * Convert.ToDecimal(this.txtPrecioCompra.Text);
                        totalPagado = totalPagado + subtotal;
                        this.lblTotalPagado.Text = totalPagado.ToString("#0.00#");

                        //Agregar ese detalle al datalistado detalle
                        DataRow row = this.dtDetalle.NewRow();
                        row["idarticulo"] = Convert.ToInt32(txtIdArticulo.Text);
                        row["articulo"] = this.txtArticulo.Text;
                        row["precio_compra"] = Convert.ToDecimal(txtPrecioCompra.Text);
                        row["precio_venta"] = Convert.ToDecimal(txtPrecioVenta.Text);
                        row["stock_inicial"] = Convert.ToInt32(txtStock.Text);
                        row["fecha_produccion"] =dtFechaProduccion.Value;
                        row["fecha_vencimiento"] = dtFechaVencimiento.Value;
                        row["subtotal"] = subtotal;
                        this.dtDetalle.Rows.Add(row);
                        this.limpiarDetalleIngreso();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = this.dataListadoDetalles.CurrentCell.RowIndex;
                DataRow row = this.dtDetalle.Rows[indiceFila];
                //Disminuir total pagado
                this.totalPagado = totalPagado - Convert.ToDecimal(row["subtotal"].ToString());
                this.lblTotalPagado.Text =totalPagado.ToString("#0.00#");
                //Removemos la fila
                this.dtDetalle.Rows.Remove(row);


            }
            catch
            {
                MensajeError("No hay FIla para remover mihelmano");
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdIngreso.Text = Convert.ToString(dataListado.CurrentRow.Cells["idingreso"].Value);
            this.txtIdProveedor.Text = Convert.ToString(dataListado.CurrentRow.Cells["proveedor"].Value);
            this.dtFecha.Value = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha"].Value);
            this.cboTipoComprobante.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(dataListado.CurrentRow.Cells["serie"].Value);
            this.txtCoreelativo.Text = Convert.ToString(dataListado.CurrentRow.Cells["correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(dataListado.CurrentRow.Cells["total"].Value);
            this.MostrarDetalles();
            this.tabControl1.SelectedIndex = 1;

        }

        private void dataListadoDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnmostrar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }
    }
}
