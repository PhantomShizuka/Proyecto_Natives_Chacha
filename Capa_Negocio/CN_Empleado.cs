using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Empleado
    {
        private CD_Empleado cD_Empleado= new CD_Empleado();
        public List<Empleado> Listar()
        {
            return cD_Empleado.Listar();
        }
    }
}
