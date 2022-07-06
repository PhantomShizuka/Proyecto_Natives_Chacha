using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Empleado oEmpleado { get; set; }
        public Rol oRol { get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
