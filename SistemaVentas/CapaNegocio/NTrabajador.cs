using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public  class NTrabajador
    {

        public static string Insertar(string nombre, string apellidos, string sexo, DateTime fecha_nacimiento,
       string num_documento, string direccion, string telefono, string email, string acceso,
       string usuario, string password)
        {
            DTrabajador obj = new DTrabajador();
            obj.Nombre = nombre;
            obj.Apellidos = apellidos;
            obj.Sexo = sexo;
            obj.Fecha_Nacimiento = fecha_nacimiento;            
            obj.Num_Documento = num_documento;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Email = email;
            obj.Acceso = acceso;
            obj.Usuario = usuario;
            obj.Password = password;
             

            return obj.Insertar(obj);

        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idtrabajador, string nombre, string apellidos, string sexo, DateTime fecha_nacimiento,
       string num_documento, string direccion, string telefono, string email, string acceso,
       string usuario, string password)
        {
            DTrabajador obj1 = new DTrabajador();
            obj1.IdTrabajador = idtrabajador;
            obj1.Nombre = nombre;
            obj1.Apellidos = apellidos;
            obj1.Sexo = sexo;
            obj1.Fecha_Nacimiento = fecha_nacimiento;
            obj1.Num_Documento = num_documento;
            obj1.Direccion = direccion;
            obj1.Telefono = telefono;
            obj1.Email = email;
            obj1.Acceso = acceso;
            obj1.Usuario = usuario;
            obj1.Password = password;


            return obj1.Editar(obj1);
        }

        //Metodo Eliminar
        public static string Eliminar(int idtrabjador)
        {
            DTrabajador obj3 = new DTrabajador();
            obj3.IdTrabajador = idtrabjador;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DTrabajador().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarApellidos(string textob)
        {
            DTrabajador obj5 = new DTrabajador();
            obj5.TextoBuscar = textob;
            return obj5.Buscar_Apellidos(obj5);

        }

        public static DataTable BuscarNum_Documento(string textob)
        {
            DTrabajador obj5 = new DTrabajador();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNum_Documento(obj5);

        }

        public static DataTable Login(string usuario, string password)
        {
            DTrabajador obj5 = new DTrabajador();
            obj5.Usuario = usuario;
            obj5.Password = password;
            return obj5.Login(obj5);

        }



    }
}
