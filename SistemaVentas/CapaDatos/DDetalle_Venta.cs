using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DDetalle_Venta
    {
        private int _Iddetalle_Venta;
        private int _IdVenta;
        private int _Iddetalle_ingreso;
        private int _Cantidad;
        private decimal _Precio_Venta;
        private decimal _Descuento;

        public int Iddetalle_Venta
        {
            get
            {
                return _Iddetalle_Venta;
            }

            set
            {
                _Iddetalle_Venta = value;
            }
        }

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

        public int Iddetalle_ingreso
        {
            get
            {
                return _Iddetalle_ingreso;
            }

            set
            {
                _Iddetalle_ingreso = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                _Cantidad = value;
            }
        }

        public decimal Precio_Venta
        {
            get
            {
                return _Precio_Venta;
            }

            set
            {
                _Precio_Venta = value;
            }
        }

        public decimal Descuento
        {
            get
            {
                return _Descuento;
            }

            set
            {
                _Descuento = value;
            }
        }

        public DDetalle_Venta()
        {
                
        }

        public DDetalle_Venta(int iddetalle_Venta,int id_venta,int iddetalle_ingreso,
            int cantidad, decimal precio_venta, decimal descuento)
        {
            this.Iddetalle_ingreso = iddetalle_ingreso;
            this.IdVenta = id_venta;
            this.Iddetalle_ingreso = iddetalle_ingreso;
            this.Cantidad = cantidad;
            this.Precio_Venta = precio_venta;
            this.Descuento = descuento;

                
        }

        //Metodo Insertar
        public string Insertar(DDetalle_Venta Detalle_Venta,
          ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            try
            {

                //Establecer el comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_detalle_venta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdedetalle_Venta = new SqlParameter();
                ParIdedetalle_Venta.ParameterName = "@iddetalle_venta";
                ParIdedetalle_Venta.SqlDbType = SqlDbType.Int;
                ParIdedetalle_Venta.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdedetalle_Venta);

                SqlParameter ParIdVenta = new SqlParameter();
                ParIdVenta.ParameterName = "@idventa";
                ParIdVenta.SqlDbType = SqlDbType.Int;
                ParIdVenta.Value = Detalle_Venta.IdVenta;
                SqlCmd.Parameters.Add(ParIdVenta);

                SqlParameter ParIddetalle_ingreso = new SqlParameter();
                ParIddetalle_ingreso.ParameterName = "@iddetalle_ingreso";
                ParIddetalle_ingreso.SqlDbType = SqlDbType.Int;
                ParIddetalle_ingreso.Value = Detalle_Venta.Iddetalle_ingreso;
                SqlCmd.Parameters.Add(ParIddetalle_ingreso);

                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@cantidad";
                ParCantidad.SqlDbType = SqlDbType.Int;
                ParCantidad.Value = Detalle_Venta.Cantidad;
                SqlCmd.Parameters.Add(ParCantidad);

                SqlParameter ParPrecioVenta = new SqlParameter();
                ParPrecioVenta.ParameterName = "@precio_venta";
                ParPrecioVenta.SqlDbType = SqlDbType.Money;
                ParPrecioVenta.Value = Detalle_Venta.Precio_Venta;
                SqlCmd.Parameters.Add(ParPrecioVenta);

                SqlParameter ParDescuento = new SqlParameter();
                ParDescuento.ParameterName = "@descuento";
                ParDescuento.SqlDbType = SqlDbType.Money;
                ParDescuento.Value = Detalle_Venta.Descuento;
                SqlCmd.Parameters.Add(ParDescuento);



                //Ejcutamos el comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se INSERTÓ el Registro";

               

            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }


            return rpta;
        }




    }
}
