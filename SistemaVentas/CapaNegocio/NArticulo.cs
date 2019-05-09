using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;


namespace CapaNegocio
{
    public class NArticulo
    {
        //Metodo que llama al método insertar de la clase DCategoria

        public static string Insertar( string codigo,string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo obj = new DArticulo();
            obj.Codigo = codigo;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            obj.Imagen = imagen;
            obj.Idcategoria = idcategoria;
            obj.Idpresentacion = idpresentacion;
            return obj.Insertar(obj);

        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idarticulo,string codigo, string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo Obj2 = new DArticulo();
            Obj2.Idarticulo = idarticulo;
            Obj2.Codigo = codigo;
            Obj2.Nombre = nombre;
            Obj2.Descripcion = descripcion;
            Obj2.Imagen = imagen;
            Obj2.Idcategoria = idcategoria;
            Obj2.Idpresentacion = idpresentacion;
            return Obj2.Editar(Obj2);
        }

        //Metodo Eliminar
        public static string Eliminar(int idarticulo)
        {
            DArticulo obj3 = new DArticulo();
            obj3.Idarticulo = idarticulo;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DArticulo().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarNombre(string textob)
        {
            DArticulo obj5 = new DArticulo();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNombre(obj5);

        }

        public static DataTable Stock()
        {
            return new DArticulo().Stock();
        }





    }
}
