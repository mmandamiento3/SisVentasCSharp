using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class Conexion
    {
        //Se ahce referencia a la clase app.config, ya que esa se puede modificar al momento
        //de instalar, si embargo la clase CONEXIOn seguira sin alterarse.
        public static string Cn = Properties.Settings.Default.cn;
        
    }
    

}

