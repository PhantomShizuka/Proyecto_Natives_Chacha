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
