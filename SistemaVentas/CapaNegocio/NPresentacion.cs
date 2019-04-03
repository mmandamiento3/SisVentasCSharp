using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio

{
    public class NPresentacion
    {
          //Metodo que llama al método insertar de la clase DCategoria
       
        public static string Insertar(string nombre, string descripcion)
        {
            DPresentacion obj = new DPresentacion();
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            return obj.Insertar(obj);           
            
        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idpresentacion, string nombre, string descripcion)
        {
            DPresentacion Obj2 = new DPresentacion();
            Obj2.IdPresentacion = idpresentacion;
            Obj2.Nombre = nombre;
            Obj2.Descripcion = descripcion;
            return Obj2.Editar(Obj2);
        }

        //Metodo Eliminar
        public static string Eliminar(int idpresentacion)
        {
            DPresentacion obj3 = new DPresentacion();
            obj3.IdPresentacion = idpresentacion;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar() {

            return new DPresentacion().Mostrar();

       }

        //budcsr nombre

        public static DataTable BuscarNombre(string textob)
        {
            DPresentacion obj5 = new DPresentacion();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNombre(obj5);

        }





    }
}
