using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public class NIngreso
    {

        public static string Insertar(int idtrabajador, int idproveedor, DateTime fecha,
            string tipo_comprobante, string serie, string corelativo,
            decimal igv, string estado, DataTable dtDetalle)
        {
            DIngreso obj = new DIngreso();
            obj.IdTrabajador = idtrabajador;
            obj.Idproveedor = idproveedor;
            obj.Tipo_comprobante = tipo_comprobante;
            obj.Serie = serie;
            obj.Correlativo = corelativo;
            obj.IGV = igv;
            obj.Estado = estado;

            List<DDetalle_Ingreso> detalles = new List<DDetalle_Ingreso>();
            foreach (DataRow row in dtDetalle.Rows)
            {
                DDetalle_Ingreso dt = new DDetalle_Ingreso();
                dt.Idarticulo = Convert.ToInt32(row["idarticulo"].ToString());
                dt.Precio_Compra= Convert.ToDecimal(row["precio_compra"].ToString()); 
                dt.Precio_venta= Convert.ToDecimal(row["precio_venta"].ToString());
                dt.Stock_inicial= Convert.ToInt32(row["stock_inicial"].ToString());
                dt.Stock_actual= Convert.ToInt32(row["stock_inicial"].ToString());
                dt.Fecha_Produccion= Convert.ToDateTime(row["fecha_produccion"].ToString());
                dt.Fecha_Vencimiento= Convert.ToDateTime(row["fecha_vencimiento"].ToString());
                detalles.Add(dt);
            }

           return obj.Insertar(obj,detalles);
                        
        }
        public static string Anular(int idingreso)
        {
            DIngreso obj3 = new DIngreso();
            obj3.IdIngreso = idingreso;
            return obj3.ANular(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DIngreso().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarFEchas(string FEchainicio1, string FEchafin1)
        {
            DIngreso obj5 = new DIngreso();
            return obj5.BuscarFechas(FEchainicio1, FEchafin1);

        }

        public static DataTable MostrarDetalle(string textobuscar)
        {
            DIngreso obj = new DIngreso();
            return obj.MostrarDetalle(textobuscar);
        }




    }
}
