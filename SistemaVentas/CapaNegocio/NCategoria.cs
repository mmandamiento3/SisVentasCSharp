using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NCategoria
    {
        //Metodo que llama al método insertar de la clase DCategoria
       
        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria obj = new DCategoria();
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Insertar(obj);           
            
        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idcategoria, string nombre, string descripcion)
        {
            DCategoria Obj2 = new DCategoria();
            Obj2.Idcategoria = idcategoria;
            Obj2.Nombre = nombre;
            Obj2.Descripcion = descripcion;
            return Obj2.Editar(Obj2);
        }

        //Metodo Eliminar
        public static string Eliminar(int idproveedor)
        {
            DCategoria obj3 = new DCategoria();
            obj3.Idcategoria = idproveedor;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DCategoria().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarNombre(string textob)
        {
            DCategoria obj5 = new DCategoria();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNombre(obj5);

        }

       


    }
}
