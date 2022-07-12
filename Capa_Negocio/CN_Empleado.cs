using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public static class CN_Empleado
    {
        public static List<Empleado> Listar => CD_Empleado.Listar;

        public static int Registrar(Empleado empleado, out string mensaje)
        {
            if (MensajeError(empleado, out mensaje))
                return 0;
            else
                return CD_Empleado.Registrar(empleado, out mensaje);
        }  

        public static bool Editar(Empleado empleado, out string mensaje)
        {
            if (MensajeError(empleado, out mensaje))
                return false;
            else
                return CD_Empleado.Editar(empleado, out mensaje);
        }

        public static bool Eliminar(Empleado empleado, out string mensaje)
        {
            if (MensajeError(empleado, out mensaje))
                return false;
            else
                return CD_Empleado.Eliminar(empleado, out mensaje);
        }
            
        public static bool MensajeError(Empleado empleado, out string mensaje)
        {
            mensaje = string.Empty;

            if (empleado.Documento == "")
                mensaje += "Es necesario el documento del empleado\n";

            if (empleado.NombreCompleto == "")
                mensaje += "Es necesario el nombre completo del empleado\n";

            if (empleado.Telefono == "")
                mensaje += "Es necesario el telefono del empleado\n";

            if (empleado.Correo == "")
                mensaje += "Es necesario el correo del empleado\n";

            if (mensaje == string.Empty)
                return false;
            else
                return true;
        }

        public static List<Empleado> EmpleadoSinUsuario()
        {
            List<Empleado> empleados = CD_Empleado.Listar;

            foreach (Usuario usuario in CD_Usuario.Listar)
            {
                foreach (Empleado empleado in empleados)
                {
                    if (usuario.oEmpleado.IdEmpleado == empleado.IdEmpleado)
                    {
                        empleados.Remove(empleado);
                        break;
                    } 
                }                    
            }

            return empleados;
        }
    }
}