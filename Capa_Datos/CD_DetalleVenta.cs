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
    public class CD_DetalleVenta
    {
        public List<DetalleVenta> Listar()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdDetalleVenta, IdVenta, IdProducto, PrecioVenta, Cantidad, SubTotal from DetalleVenta";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                IdDetalleVenta = Convert.ToInt32(reader["IdDetalleVenta"]),
                                IdVenta = Convert.ToInt32(reader["IdVenta"]),
                                oProducto = new CD_Producto().GetProducto(Convert.ToInt32(reader["IdProducto"])),
                                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"])
                            });
                        }
                    }

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    lista = new List<DetalleVenta>();
                }
            }

            return lista;
        }
        public List<DetalleVenta> GetListaDetalleVenta(int uIdVenta)
        {
            return new CD_DetalleVenta().Listar().Where(d => d.IdVenta == uIdVenta).ToList();
        }
    }
}
