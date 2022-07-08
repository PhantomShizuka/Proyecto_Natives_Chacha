using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class DetalleProducto
    {
        public int IdDetalleProducto { get; set; }
        public int IdProducto { get; set; }
        public Insumo oInsumo { get; set; }
        public int Cantidad { get; set; }
        public string FechaRegistro { get; set; }
    }
}
