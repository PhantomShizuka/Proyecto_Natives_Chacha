using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public string FechRegistro { get; set; }
        public List<Permiso> oListaPermiso { get; set; }
    }
}
