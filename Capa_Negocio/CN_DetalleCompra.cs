using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Datos;

namespace Capa_Negocio
{
    public static class CN_DetalleCompra
    {
        public static List<DetalleCompra> Listar => CD_DetalleCompra.Listar;
    }
}
