using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Usuario
    {
        public List<Usuario> Listar()
        {
            return new CD_Usuario().Listar();
        }
        public bool UsuarioValido(string contraseña, string usuario, out Usuario ousuario)
        {
            ousuario = new CN_Usuario().Listar().Where(u => u.Contraseña == contraseña && u.oEmpleado.Documento == usuario).FirstOrDefault();

            if (ousuario != null)
                return true;
            else
                return false;
        }
    }

}
