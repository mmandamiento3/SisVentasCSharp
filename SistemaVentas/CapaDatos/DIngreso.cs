using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DIngreso
    {
        private int _IdIngreso;
        private int _IdTrabajador;
        private int _Idproveedor;
        private DateTime _Fecha;
        private string _Tipo_comprobante;
        private string _Serie;
        private string _Correlativo;
        private decimal _IGV;
        private string _Estado;

        public int IdIngreso
        {
            get
            {
                return _IdIngreso;
            }

            set
            {
                _IdIngreso = value;
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

        public int Idproveedor
        {
            get
            {
                return _Idproveedor;
            }

            set
            {
                _Idproveedor = value;
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

        public string Tipo_comprobante
        {
            get
            {
                return _Tipo_comprobante;
            }

            set
            {
                _Tipo_comprobante = value;
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

        public string Estado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }

        public DIngreso()
        {

        }

        public DIngreso(int idingreso, int idtrabajador, int idproveedor,
            DateTime fecha, string tipo_comprobante, string serie, 
            string correlativo, decimal igv, string estado)
        {
            this.IdIngreso = idingreso;
            this.IdTrabajador = idtrabajador;
            this.Idproveedor = idproveedor;
            this.Fecha = fecha;
            this.Tipo_comprobante = tipo_comprobante;
            this.Serie = serie;
            this.Correlativo = correlativo;
            this.IGV = igv;
            this.Estado = estado;
        }

        //METODOS

        public string Insertar(DIngreso Ingreso, List<DDetalle_Ingreso> Detalle)
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
                SqlCmd.CommandText = "spinsertar_ingreso";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdIngreso = new SqlParameter();
                ParIdIngreso.ParameterName = "@idingreso";
                ParIdIngreso.SqlDbType = SqlDbType.Int;
                ParIdIngreso.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdIngreso);

                SqlParameter ParIdtrabajador = new SqlParameter();
                ParIdtrabajador.ParameterName = "@idtrabajador";
                ParIdtrabajador.SqlDbType = SqlDbType.Int;
                ParIdtrabajador.Value = Ingreso.IdTrabajador;
                SqlCmd.Parameters.Add(ParIdtrabajador);

                SqlParameter ParIdProveedor = new SqlParameter();
                ParIdProveedor.ParameterName = "@idproveedor";
                ParIdProveedor.SqlDbType = SqlDbType.Int;
                ParIdProveedor.Value = Ingreso.Idproveedor;
                SqlCmd.Parameters.Add(ParIdProveedor);


                SqlParameter ParFecha = new SqlParameter();
                ParFecha.ParameterName = "@fecha";
                ParFecha.SqlDbType = SqlDbType.Date;
                ParFecha.Value = Ingreso.Fecha;
                SqlCmd.Parameters.Add(ParFecha);

                SqlParameter ParTipo_Comprobante = new SqlParameter();
                ParTipo_Comprobante.ParameterName = "@tipo_comprobante";
                ParTipo_Comprobante.SqlDbType = SqlDbType.VarChar;
                ParTipo_Comprobante.Size = 20;
                ParTipo_Comprobante.Value = Ingreso.Tipo_comprobante;
                SqlCmd.Parameters.Add(ParTipo_Comprobante);

                SqlParameter ParSerie = new SqlParameter();
                ParSerie.ParameterName = "@serie";
                ParSerie.SqlDbType = SqlDbType.VarChar;
                ParSerie.Size = 4;
                ParSerie.Value = Ingreso.Serie;
                SqlCmd.Parameters.Add(ParSerie);

                SqlParameter ParCorrelativo = new SqlParameter();
                ParCorrelativo.ParameterName = "@correlativo";
                ParCorrelativo.SqlDbType = SqlDbType.VarChar;
                ParCorrelativo.Size = 7;
                ParCorrelativo.Value = Ingreso.Correlativo;
                SqlCmd.Parameters.Add(ParCorrelativo);

                SqlParameter ParIGV = new SqlParameter();
                ParIGV.ParameterName = "@igv";
                ParIGV.SqlDbType = SqlDbType.Decimal;
                ParIGV.Precision = 4;
                ParIGV.Scale = 2;
                ParIGV.Value = Ingreso.IGV;
                SqlCmd.Parameters.Add(ParIGV);

                SqlParameter ParEstado = new SqlParameter();
                ParEstado.ParameterName = "@estado";
                ParEstado.SqlDbType = SqlDbType.VarChar;
                ParEstado.Size = 7;
                ParEstado.Value = Ingreso.Estado;
                SqlCmd.Parameters.Add(ParEstado);

                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se INSERTÓ el Registro";

                if (rpta.Equals("OK"))
                {
                    //Obtenemos el codigo del ingreso generado
                    this.IdIngreso = Convert.ToInt32(SqlCmd.Parameters["@idingreso"].Value);

                    foreach (DDetalle_Ingreso det in Detalle)
                    {
                        det.Idingreso = this.IdIngreso;
                        //llamamos al metodo insertar de la clase detalle de ingreso
                        rpta = det.Insertar(det, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
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


        //Metodo Anular

            public string ANular(DIngreso Ingreso)
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
                SqlCmd.CommandText = "spanular_ingreso";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdIngreso = new SqlParameter();
                ParIdIngreso.ParameterName = "@idingreso";
                ParIdIngreso.SqlDbType = SqlDbType.Int;
                ParIdIngreso.Value = Ingreso.IdIngreso;
                SqlCmd.Parameters.Add(ParIdIngreso);



                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se ELIMINÓ el Registro";




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
            DataTable DtResultado = new DataTable("ingreso");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spmostrar_ingreso";
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
            DataTable DtResultado = new DataTable("ingreso");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spbuscar_ingreso_fecha";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParFEchaInicio = new SqlParameter();
                ParFEchaInicio.ParameterName = "@fechaInicio";
                ParFEchaInicio.SqlDbType = SqlDbType.VarChar;
                ParFEchaInicio.Size = 50;
                ParFEchaInicio.Value = Fechainicio;
                SqlCmd.Parameters.Add(ParFEchaInicio);

                SqlParameter ParFechafin = new SqlParameter();
                ParFechafin.ParameterName = "@fechaFin";
                ParFechafin.SqlDbType = SqlDbType.VarChar;
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
            DataTable DtResultado = new DataTable("detalle_ingreso");//nombre de la tabla como parametro
            SqlConnection SQlCon = new SqlConnection();
            try
            {
                SQlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SQlCon;
                SqlCmd.CommandText = "spmostrar_detalle_ingreso";
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

