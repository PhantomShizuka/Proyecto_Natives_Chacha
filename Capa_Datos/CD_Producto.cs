using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdProducto, IdCategoria, Nombre, Descripcion, Stock, PrecioElaboracion, PrecioVenta, Estado from Producto";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                oCategoria = new CD_Categoria().GetCategoria(Convert.ToInt32(reader["IdCategoria"])),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                PrecioElaboracion = Convert.ToDecimal(reader["PrecioElaboracion"]),
                                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                oDetalleProducto = new CD_DetalleProducto().GetListaDetalleProducto(Convert.ToInt32(reader["IdProducto"]))
                            });
                        }
                    }

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    lista = new List<Producto>();
                }
            }

            return lista;
        }
        public Producto GetProducto(int uIdProducto)
        {
            return new CD_Producto().Listar().Where(p => p.IdProducto == uIdProducto).FirstOrDefault();
        }
    }
}
