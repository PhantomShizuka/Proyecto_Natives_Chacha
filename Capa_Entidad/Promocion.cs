using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Promocion
    {
        public int IdPromocion { get; set; }
        public int DescuentoFijo { get; set; }
        public decimal DescuentoPorcentual { get; set; }
        public bool Estado { get; set; }
        public bool TipoDescuento { get; set; }
        public string FechaRegistro { get; set; }
    }
}
