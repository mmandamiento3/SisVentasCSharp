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
            this.ttMensaje.SetToolTip(this.txtIdCLiente,"Seleccione un Cliente");
            this.ttMensaje.SetToolTip(this.txtSerie,"Ingrese una Serie del Comprobante");
            this.ttMensaje.SetToolTip(this.txtCoreelativo,"Ingrese el nùmero del comprobante");
            this.ttMensaje.SetToolTip(this.txtCantidad,"Ingrese la Cantidad del Articulos");
            this.ttMensaje.SetToolTip(this.txtArticulo,"Seleccione un articulo");
            this.txtIdCLiente.Visible = false;
            this.txtIdArticulo.Visible = false;
            this.txtIdCLiente.ReadOnly = true;
            this.txtIdArticulo.ReadOnly = true;
            this.dtFechaVencimiento.Enabled = false;
            this.txtPrecioCompra.ReadOnly = true;
            this.txtStockActual.ReadOnly = true;
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
            this.txtIdVenta.Text = string.Empty;
            this.txtIdCLiente.Text = string.Empty;
            this.txtCliente.Text = string.Empty;
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
            this.txtStockActual.Text = string.Empty;
            this.txtCantidad.Text = string.Empty;
            this.txtPrecioCompra.Text = string.Empty;
            this.txtPrecioVenta.Text = string.Empty;
            this.txtDescuento.Text = string.Empty;

        }

        //Habilitar los controles dle form
        private void HabilitarCajasTexto(bool valor)
        {
            this.txtIdVenta.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCoreelativo.ReadOnly = !valor;
            this.txtIGV.ReadOnly = !valor;
            this.dtFecha.Enabled = valor;
            this.cboTipoComprobante.Enabled = valor;
            this.txtCantidad.ReadOnly = !valor;
            this.txtPrecioCompra.ReadOnly = !valor;
            this.txtPrecioVenta.ReadOnly = !valor;
            this.txtStockActual.ReadOnly = valor;
            this.txtDescuento.ReadOnly = !valor;
            this.dtFechaVencimiento.Enabled = valor;

            this.btnBuscarArticulo.Enabled = valor;
            this.btnBuscarCliente.Enabled = valor;
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
            this.dataListado.DataSource = NVenta.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo BuscarNombre
        private void BuscarFechas()
        {
            this.dataListado.DataSource = NVenta.BuscarFEchas(this.dtFecha1.Value.ToString("dd/MM/yyyy"), this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);

        }

        private void MostrarDetalles()
        {
            this.dataListadoDetalles.DataSource = NVenta.MostrarDetalle(this.txtIdVenta.Text);

        }

        public void CrearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("iddetalle_ingreso", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("precio_venta", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("descuento", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));

            //Relacionando/Agregando nuestro datagridview con nuestra datatable
            this.dataListadoDetalles.DataSource = this.dtDetalle;

        }





        private void frmVenta_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.HabilitarCajasTexto(false);
            this.BotonesHabilitados();
            this.CrearTabla();
        }

        private void frmVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmVistaCliente_Venta vista = new frmVistaCliente_Venta();
            vista.ShowDialog();
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            frmVistaArticulo_Venta vista2 = new frmVistaArticulo_Venta();
            vista2.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                Opcion = MessageBox.Show("Desea realmente ELiminar los Registros?", "Sistema de VEntas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows) //Recorremos con un bucle las filas del datalistado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) //SI encontramos alguna fila seleccionada [0] = Eliminar
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value); //Codigo sera igual a la celda[1] que es la llave idcategoria;
                            rpta = NVenta.Eliminar(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se ELiminooOoo  Correctamente el Registro");
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

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdVenta.Text = Convert.ToString(dataListado.CurrentRow.Cells["idventa"].Value);
            this.txtCliente.Text = Convert.ToString(dataListado.CurrentRow.Cells["cliente"].Value);
            this.dtFecha.Value = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha"].Value);
            this.cboTipoComprobante.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(dataListado.CurrentRow.Cells["serie"].Value);
            this.txtCoreelativo.Text = Convert.ToString(dataListado.CurrentRow.Cells["correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(dataListado.CurrentRow.Cells["total"].Value);
            this.MostrarDetalles();
            this.tabControl1.SelectedIndex = 1;

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
            this.limpiarDetalleIngreso();
            this.HabilitarCajasTexto(true);
            this.txtSerie.Focus();
            this.txtIdVenta.Enabled = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.BotonesHabilitados();
            this.LimpiarControles();
            this.limpiarDetalleIngreso();
            this.HabilitarCajasTexto(false);
            this.txtSerie.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtCliente.Text == string.Empty || this.txtSerie.Text == string.Empty
                    || this.txtCoreelativo.Text == string.Empty || this.txtIGV.Text == string.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos serán remarcados");
                    errorIcono.SetError(txtCliente, "Ingrese un valor");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(txtSerie, "Ingrese un valor");
                    errorIcono.SetError(txtCoreelativo, "Ingrese un valor");
                    errorIcono.SetError(txtIGV, "Ingrese un valor");


                }
                else
                {
                    //Enviar la imagen

                    if (this.IsNuevo)
                    {
                        rpta = NVenta.Insertar(Convert.ToInt32(this.txtIdCLiente.Text), IdTrbajador,
                           dtFecha.Value, cboTipoComprobante.Text, txtSerie.Text, txtCoreelativo.Text,
                           Convert.ToDecimal(txtIGV.Text), dtDetalle);//Trim elimina los espacios en blanco   


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

                if (this.txtIdArticulo.Text == string.Empty || this.txtCantidad.Text == string.Empty || this.txtDescuento.Text == string.Empty
                    || this.txtPrecioVenta.Text == string.Empty)
                {
                    MensajeError("Falta Ingresar algunos datos remarcados");
                    errorIcono.SetError(txtIdArticulo, "Ingrese un valor");//Muestra el icono de error al costado de la caja de texto
                    errorIcono.SetError(txtCantidad, "Ingrese un valor");
                    errorIcono.SetError(txtDescuento, "Ingrese un valor");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese un valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["iddetalle_ingreso"]) == Convert.ToInt32(this.txtIdArticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el articulo en el detalle");
                        }
                    }

                    if (registrar && Convert.ToInt32(txtCantidad.Text)<=Convert.ToInt32(this.txtStockActual.Text))
                    {
                        decimal subtotal = Convert.ToDecimal(this.txtCantidad.Text) * Convert.ToDecimal(this.txtPrecioVenta.Text) - Convert.ToDecimal(this.txtDescuento.Text);
                        TotalPagado = TotalPagado + subtotal;
                        this.lblTotalPagado.Text = TotalPagado.ToString("#0.00#");

                        //Agregar ese detalle al datalistado detalle
                        DataRow row = this.dtDetalle.NewRow();
                        row["iddetalle_ingreso"] = Convert.ToInt32(txtIdArticulo.Text);
                        row["articulo"] = this.txtArticulo.Text;
                        row["cantidad"] = Convert.ToInt32(txtCantidad.Text);
                        row["precio_venta"] = Convert.ToDecimal(txtPrecioVenta.Text);                       
                        row["descuento"] = Convert.ToDecimal(this.txtDescuento.Text);
                        row["subtotal"] = subtotal;
                        this.dtDetalle.Rows.Add(row);
                        this.limpiarDetalleIngreso();
                    }
                    else
                    {
                        MensajeError("No hay STOCK Suficiente mihelma");
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
                this.TotalPagado = TotalPagado - Convert.ToDecimal(row["subtotal"].ToString());
                this.lblTotalPagado.Text = TotalPagado.ToString("#0.00#");
                //Removemos la fila
                this.dtDetalle.Rows.Remove(row);


            }
            catch
            {
                MensajeError("No hay FIla para remover mihelmano");
            }
        }

        private void btnComprobante_Click(object sender, EventArgs e)
        {
            frmReporteFactura frm = new frmReporteFactura();
            frm.Idventa = Convert.ToInt32(this.dataListado.CurrentRow.Cells["idventa"].Value);
            frm.ShowDialog();
        }
    }
    }

