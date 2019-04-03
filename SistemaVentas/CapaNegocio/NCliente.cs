using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using CapaDatos;

namespace CapaNegocio
{
    public class NCliente
    {
        //Metodos para comunicarnos con la capaDAtps
        public static string Insertar(string nombre, string apellidos, string sexo,DateTime fecha_nacimiento,
       string tipo_Documento, string num_documento, string direccion, string telefono, string email)
        {
            DCliente obj = new DCliente();
            obj.Nombre = nombre;
            obj.Apellidos = apellidos;
            obj.Sexo = sexo;
            obj.Fecha_Nacimiento = fecha_nacimiento;
            obj.Tipo_Documento = tipo_Documento;
            obj.Num_Documento = num_documento;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Email = email;
           
            return obj.Insertar(obj);

        }

        //Método Editar que llama al metodo editar de la clase DCategoria

        public static string Editar(int idcliente, string nombre, string apellidos, string sexo, DateTime fecha_nacimiento,
       string tipo_Documento, string num_documento, string direccion, string telefono, string email)
            {
            DCliente obj1 = new DCliente();
            obj1.IdCliente = idcliente;
            obj1.Nombre = nombre;
            obj1.Apellidos = apellidos;
            obj1.Sexo = sexo;
            obj1.Fecha_Nacimiento = fecha_nacimiento;
            obj1.Tipo_Documento = tipo_Documento;
            obj1.Num_Documento = num_documento;
            obj1.Direccion = direccion;
            obj1.Telefono = telefono;
            obj1.Email = email;
            

            return obj1.Editar(obj1);
        }

        //Metodo Eliminar
        public static string Eliminar(int idcliente)
        {
            DCliente obj3 = new DCliente();
            obj3.IdCliente = idcliente;
            return obj3.Eliminar(obj3);
        }

        //Metod mostrar 
        public static DataTable Mostrar()
        {

            return new DCliente().Mostrar();

        }

        //budcsr nombre

        public static DataTable BuscarApellidos(string textob)
        {
            DCliente obj5 = new DCliente();
            obj5.TextoBuscar = textob;
            return obj5.Buscar_Apellidos(obj5);

        }

        public static DataTable BuscarNum_Documento(string textob)
        {
            DCliente obj5 = new DCliente();
            obj5.TextoBuscar = textob;
            return obj5.BuscarNum_Documento(obj5);

        }

    }
}
