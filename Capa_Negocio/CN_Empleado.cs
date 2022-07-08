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
        public List<Empleado> Listar()
        {
            return new CD_Empleado().Listar();
        }
    }
}
