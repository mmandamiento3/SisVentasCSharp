using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NVenta
    {
        public static string Insertar(int idcliente, int idtrabajador, DateTime fecha,
           string tipo_comprobante, string serie, string correlativo,
           decimal igv, DataTable dtDetalle)
        {
            DVentas obj = new DVentas();
            obj.IdCliente = idcliente;
            obj.IdTrabajador = idtrabajador;
            obj.Fecha = fecha;
            obj.TIpo_Comprobante = tipo_comprobante;
            obj.Serie = serie;
            obj.Correlativo = correlativo;
            obj.IGV = igv;

            List<DDetalle_Venta> detalles = new List<DDetalle_Venta>();
            foreach (DataRow row in dtDetalle.Rows)
            {
                DDetalle_Venta dt = new DDetalle_Venta();
                dt.Iddetalle_ingreso = Convert.ToInt32(row["iddetalle_ingreso"].ToString());
                dt.Cantidad = Convert.ToInt32(row["cantidad"].ToString());
                dt.Precio_Venta = Convert.ToDecimal(row["precio_venta"].ToString());
                dt.Descuento = Convert.ToDecimal(row["descuento"].ToString());
                detalles.Add(dt);
            }

            return obj.Insertar(obj, detalles);

        }
        public static string Eliminar(int idventa)
        {
            DVentas obj3 = new DVentas();
            obj3.IdVenta = idventa;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DVentas().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarFEchas(string FEchainicio1, string FEchafin1)
        {
            DVentas obj5 = new DVentas();
            return obj5.BuscarFechas(FEchainicio1, FEchafin1);

        }

        public static DataTable MostrarDetalle(string textobuscar)
        {
            DVentas obj = new DVentas();
            return obj.MostrarDetalle(textobuscar);
        }

        public static DataTable MostrarArticulo_Venta_Nombre(string textobuscar)
        {
            DVentas obj3 = new DVentas();
            return obj3.MostrarArticulo_Venta_Nombre(textobuscar);
            
        }
        public static DataTable MostrarArticulo_Venta_Codigo(string textobuscar)
        {
            DVentas obj3 = new DVentas();
            return obj3.MostrarArticulo_Venta_Codigo(textobuscar);

        }



    }
}
