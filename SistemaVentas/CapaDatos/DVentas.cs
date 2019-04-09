using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DVentas
    {
        private int _IdVenta;
        private int _IdCliente;
        private int _IdTrabajador;
        private DateTime _Fecha;
        private string _TIpo_Comprobante;
        private string _Serie;
        private string _Correlativo;
        private decimal _IGV;

        public int IdVenta
        {
            get
            {
                return _IdVenta;
            }

            set
            {
                _IdVenta = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return _IdCliente;
            }

            set
            {
                _IdCliente = value;
            }
        }

        public int IdTrabajador
        {
            get
            {
                return _IdTrabajador;
            }

            set
            {
                _IdTrabajador = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }

            set
            {
                _Fecha = value;
            }
        }

        public string TIpo_Comprobante
        {
            get
            {
                return _TIpo_Comprobante;
            }

            set
            {
                _TIpo_Comprobante = value;
            }
        }

        public string Serie
        {
            get
            {
                return _Serie;
            }

            set
            {
                _Serie = value;
            }
        }

        public string Correlativo
        {
            get
            {
                return _Correlativo;
            }

            set
            {
                _Correlativo = value;
            }
        }

        public decimal IGV
        {
            get
            {
                return _IGV;
            }

            set
            {
                _IGV = value;
            }
        }

        public DVentas()
        {

        }

        public DVentas(int idventa, int idcliente, int idtrabajador,
            DateTime fecha, string tipo_comprobante, string serie,
            string correlativo, decimal igv)
        {
            this.IdVenta = idventa;
            this.IdCliente = idcliente;
            this.IdTrabajador = idtrabajador;
            this.Fecha = fecha;
            this.TIpo_Comprobante = tipo_comprobante;
            this.Serie = serie;
            this.Correlativo = correlativo;
            this.IGV = igv;
        }

        //METODOS

        public string Disminuir_Stock(int iddetalle_ingreso, int cantidad)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Establecer el comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdisminuir_stock";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIddetalle_Ingreso = new SqlParameter();
                ParIddetalle_Ingreso.ParameterName = "@iddetalle_ingreso";
                ParIddetalle_Ingreso.SqlDbType = SqlDbType.Int;
                ParIddetalle_Ingreso.Value = iddetalle_ingreso;
                SqlCmd.Parameters.Add(ParIddetalle_Ingreso);

                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@cantidad";
                ParCantidad.SqlDbType = SqlDbType.Int;
                ParCantidad.Value = cantidad;
                SqlCmd.Parameters.Add(ParCantidad);

                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se ACtualizó el Stock mihelma";




            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();


            }
            return rpta;
        }


        public string Insertar(DVentas Venta, List<DDetalle_Venta> Detalle)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Establecer el comando
                SqlCommand SqlCmd = new SqlCommand();
                //ESTABLECER LA TRANSACCION
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdVenta = new SqlParameter();
                ParIdVenta.ParameterName = "@idventa";
                ParIdVenta.SqlDbType = SqlDbType.Int;
                ParIdVenta.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdVenta);

                SqlParameter ParIdCliente = new SqlParameter();
                ParIdCliente.ParameterName = "@idcliente";
                ParIdCliente.SqlDbType = SqlDbType.Int;
                ParIdCliente.Value = Venta.IdCliente;
                SqlCmd.Parameters.Add(ParIdCliente);

                SqlParameter ParIdtrabajador = new SqlParameter();
                ParIdtrabajador.ParameterName = "@idtrabajador";
                ParIdtrabajador.SqlDbType = SqlDbType.Int;
                ParIdtrabajador.Value = Venta.IdTrabajador;
                SqlCmd.Parameters.Add(ParIdtrabajador);

                 SqlParameter ParFecha = new SqlParameter();
                ParFecha.ParameterName = "@fecha";
                ParFecha.SqlDbType = SqlDbType.DateTime;
                ParFecha.Value = Venta.Fecha;
                SqlCmd.Parameters.Add(ParFecha);

                SqlParameter ParTipo_Comprobante = new SqlParameter();
                ParTipo_Comprobante.ParameterName = "@tipo_comprobante";
                ParTipo_Comprobante.SqlDbType = SqlDbType.VarChar;
                ParTipo_Comprobante.Size = 20;
                ParTipo_Comprobante.Value = Venta.TIpo_Comprobante;
                SqlCmd.Parameters.Add(ParTipo_Comprobante);

                SqlParameter ParSerie = new SqlParameter();
                ParSerie.ParameterName = "@serie";
                ParSerie.SqlDbType = SqlDbType.VarChar;
                ParSerie.Size = 4;
                ParSerie.Value = Venta.Serie;
                SqlCmd.Parameters.Add(ParSerie);

                SqlParameter ParCorrelativo = new SqlParameter();
                ParCorrelativo.ParameterName = "@correlativo";
                ParCorrelativo.SqlDbType = SqlDbType.VarChar;
                ParCorrelativo.Size = 7;
                ParCorrelativo.Value = Venta.Correlativo;
                SqlCmd.Parameters.Add(ParCorrelativo);

                SqlParameter ParIGV = new SqlParameter();
                ParIGV.ParameterName = "@igv";
                ParIGV.SqlDbType = SqlDbType.Decimal;
                ParIGV.Precision = 4;
                ParIGV.Scale = 2;
                ParIGV.Value = Venta.IGV;
                SqlCmd.Parameters.Add(ParIGV);

                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se INSERTÓ el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtenemos el codigo del ingreso generado
                    this.IdVenta = Convert.ToInt32(SqlCmd.Parameters["@idventa"].Value);

                    foreach (DDetalle_Venta det in Detalle)
                    {
                        det.IdVenta = this.IdVenta; ;
                        //llamamos al metodo insertar de la clase detalle de ingreso
                        rpta = det.Insertar(det, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
                        else
                        {
                            //actualizamos el stock
                            rpta = Disminuir_Stock(det.Iddetalle_ingreso, det.Cantidad);
                            if (!rpta.Equals("OK"))
                            {
                                break;
                            }
                        }
                    }
                }
                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }



            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();


            }
            return rpta;
        }

        //Metodo  ELiminar

        public string Eliminar(DVentas Ventas)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Establecer el comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdventa = new SqlParameter();
                ParIdventa.ParameterName = "@idventa";
                ParIdventa.SqlDbType = SqlDbType.Int;
                ParIdventa.Value = Ventas.IdVenta;
                SqlCmd.Parameters.Add(ParIdventa);



                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "OK";




            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();


            }
            return rpta;
        }



        //Metodo Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("venta");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spmostrar_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);//Ejecutar el comando y llenar el datatable
                SqlDat.Fill(DtResultado);//llenar el datatable
            }

            catch (Exception e)
            {
                DtResultado = null;
            }
            return DtResultado;

        }

        //Metodo BuscarNombre
        public DataTable BuscarFechas(string Fechainicio, string FEchafin)
        {
            DataTable DtResultado = new DataTable("venta");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spbuscar_venta_fecha";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParFEchaInicio = new SqlParameter();
                ParFEchaInicio.ParameterName = "@textobuscar";
                ParFEchaInicio.SqlDbType = SqlDbType.DateTime;
                ParFEchaInicio.Size = 50;
                ParFEchaInicio.Value = Fechainicio;
                SqlCmd.Parameters.Add(ParFEchaInicio);

                SqlParameter ParFechafin = new SqlParameter();
                ParFechafin.ParameterName = "@textobuscar1";
                ParFechafin.SqlDbType = SqlDbType.DateTime;
                ParFechafin.Size = 50;
                ParFechafin.Value = FEchafin;
                SqlCmd.Parameters.Add(ParFechafin);


                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);//Ejecutar el comando y llenar el datatable
                SqlDat.Fill(DtResultado);//llenar el datatable
            }

            catch (Exception e)
            {
                DtResultado = null;
            }
            return DtResultado;


        }



        public DataTable MostrarDetalle(string textobuscar)
        {
            DataTable DtResultado = new DataTable("detalle_venta");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spmostrar_detalle_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTExtoBuscar = new SqlParameter();
                ParTExtoBuscar.ParameterName = "@textobuscar";
                ParTExtoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTExtoBuscar.Size = 50;
                ParTExtoBuscar.Value = textobuscar;
                SqlCmd.Parameters.Add(ParTExtoBuscar);




                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);//Ejecutar el comando y llenar el datatable
                SqlDat.Fill(DtResultado);//llenar el datatable
            }

            catch (Exception e)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        //Mostrar Articulo por su nombre
        public DataTable MostrarArticulo_Venta_Nombre(string textobuscar)
        {
            DataTable DtResultado = new DataTable("articulos");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spbuscararticulo_venta_nombre";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTExtoBuscar = new SqlParameter();
                ParTExtoBuscar.ParameterName = "@textobuscar";
                ParTExtoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTExtoBuscar.Size = 50;
                ParTExtoBuscar.Value = textobuscar;
                SqlCmd.Parameters.Add(ParTExtoBuscar);




                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);//Ejecutar el comando y llenar el datatable
                SqlDat.Fill(DtResultado);//llenar el datatable
            }

            catch (Exception e)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable MostrarArticulo_Venta_Codigo(string textobuscar)
        {
            DataTable DtResultado = new DataTable("articulos");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spbuscararticulo_venta_codigo";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTExtoBuscar = new SqlParameter();
                ParTExtoBuscar.ParameterName = "@textobuscar";
                ParTExtoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTExtoBuscar.Size = 50;
                ParTExtoBuscar.Value = textobuscar;
                SqlCmd.Parameters.Add(ParTExtoBuscar);




                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);//Ejecutar el comando y llenar el datatable
                SqlDat.Fill(DtResultado);//llenar el datatable
            }

            catch (Exception e)
            {
                DtResultado = null;
            }
            return DtResultado;
        }



    }
}
