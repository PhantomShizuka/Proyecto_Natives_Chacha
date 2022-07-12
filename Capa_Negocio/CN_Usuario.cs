using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public static class CN_Usuario
    {
        public static List<Usuario> Listar => CD_Usuario.Listar;

        public static int Registrar(Usuario obj, out string mensaje)
        {
            if (MensajeError(obj, out mensaje))
                return 0;
            else
                return CD_Usuario.Registrar(obj, out mensaje);
        }

        public static bool Editar(Usuario obj, out string mensaje)
        {
            if (MensajeError(obj, out mensaje))
                return false;        
            else
                return CD_Usuario.Editar(obj, out mensaje);
        }

        public static bool Eliminar(Usuario obj, out string mensaje)
        {
            if (MensajeError(obj, out mensaje))
                return false;
            else
                return CD_Usuario.Eliminar(obj, out mensaje);
        }

        public static bool MensajeError(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (usuario.Contraseña == "")
                mensaje += "Es necesario la contraseña del empleado\n";

            if (mensaje == string.Empty)
                return false;
            else
                return true;
        }

        public static bool UsuarioValido(string contraseña, string usuario, out Usuario ousuario, out string mensaje)
        {
            bool valido = false;
            mensaje = null;
            
            ousuario = Listar.FirstOrDefault(u => u.Contraseña == contraseña && u.oEmpleado.Documento == usuario);

            if (ousuario != null)
                if (ousuario.oEmpleado.Estado)
                    if (ousuario.Estado)
                        valido = true;
                    else
                        mensaje = "Usuario no activo";
                else
                    mensaje = "Empleado no activo";
            else
                mensaje = "Usuario o contraseña incorrecta";

            return valido;
        }
    }
}
