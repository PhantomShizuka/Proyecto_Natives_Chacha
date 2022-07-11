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
    public static class CD_Producto
    {
        public static List<Producto> Listar
        {
            get
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
                                    oCategoria = CD_Categoria.GetCategoria(Convert.ToInt32(reader["IdCategoria"])),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    PrecioElaboracion = Convert.ToDecimal(reader["PrecioElaboracion"]),
                                    PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    oDetalleProducto = CD_DetalleProducto.GetListaDetalleProducto(Convert.ToInt32(reader["IdProducto"]))
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
        }

        public static Producto GetProducto(int uIdProducto) => Listar.FirstOrDefault(p => p.IdProducto == uIdProducto);
    }
}
