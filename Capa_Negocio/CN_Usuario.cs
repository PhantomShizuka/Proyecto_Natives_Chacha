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
        private CD_Usuario cD_Usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return cD_Usuario.Listar();
        }
        public bool UsuarioValido(string contraseña, out Usuario ousuario)
        {
            ousuario = new CN_Usuario().Listar().Where(u => u.Contraseña == contraseña).FirstOrDefault();

            if (ousuario != null)
                return true;
            else
                return false;
        }
    }

}
