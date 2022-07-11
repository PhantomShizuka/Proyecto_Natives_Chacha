using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Datos;

namespace Capa_Negocio
{
    public static class CN_Compra
    {
        public static List<Compra> Listar => CD_Compra.Listar;
    }
}
