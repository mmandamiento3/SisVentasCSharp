using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaDatos;


namespace CapaNegocio
{
     public class NProveedor
    {

        public static string Insertar(string razon_proveedor, string sector_comercial, string  tipo_documento,
            string num_documento, string direccion, string telefono, string email, string url)
        {
            DProveedor obj = new DProveedor();
            obj.Razon_Social = razon_proveedor;
            obj.Sector_Comercial = sector_comercial;
            obj.Tipo_Documento = tipo_documento;
            obj.Num_Documento = num_documento;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Email = email;
            obj.Url = url;
            return obj.Insertar(obj);

        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idproveedor,string razon_proveedor, string sector_comercial, string tipo_documento,
            string num_documento, string direccion, string telefono, string email, string url)
        {
            DProveedor obj1 = new DProveedor();
            obj1.Idproveedor = idproveedor;
            obj1.Razon_Social = razon_proveedor;
            obj1.Sector_Comercial = sector_comercial;
            obj1.Tipo_Documento = tipo_documento;
            obj1.Num_Documento = num_documento;
            obj1.Direccion = direccion;
            obj1.Telefono = telefono;
            obj1.Email = email;
            obj1.Url = url; 

            return obj1.Editar(obj1);
        }

        //Metodo Eliminar
        public static string Eliminar(int idproveedor)
        {
            DProveedor obj3 = new DProveedor();
            obj3.Idproveedor = idproveedor;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DProveedor().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarRazon_Social(string textob)
        {
            DProveedor obj5 = new DProveedor();
            obj5.TextoBuscar = textob;
            return obj5.BuscarRazon_Social(obj5);

        }

        public static DataTable BuscarNum_Documento(string textob)
        {
            DProveedor obj5 = new DProveedor();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNum_Documento(obj5);

        }




    }
}
