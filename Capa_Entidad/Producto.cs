using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Producto
    {
        public int Id { get; set; }
        public Categoria oCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal PrecioElaboracion { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Estado { get; set; }
        public string FechRegistro { get; set; }
        public List<DetalleProducto> oDetalleProducto { get; set; }
    }
}
