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
    public static class CD_Venta
    {
        public static List<Venta> Listar
        {
            get
            {
                List<Venta> lista = new List<Venta>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdVenta, IdUsuario, IdPromocion, TipoDocumento, NmrDocumento, DocumentoCliente, NombreCliente, MontoPago, MontoCambio, MontoTotal from Venta";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Venta()
                                {
                                    IdVenta = Convert.ToInt32(reader["IdVenta"]),
                                    oUsuario = CD_Usuario.GetUsuario(Convert.ToInt32(reader["IdUsuario"])),
                                    oPromocion = CD_Promocion.GetPromocion(Convert.ToInt32(reader["IdPromocion"])),
                                    TipoDocumento = reader["TipoDocumento"].ToString(),
                                    NmrDocumento = reader["NmrDocumento"].ToString(),
                                    DocumentoCliente = reader["DocumentoCliente"].ToString(),
                                    NombreCliente = reader["NombreCliente"].ToString(),
                                    MontoPago = Convert.ToDecimal(reader["MontoPago"]),
                                    MontoCambio = Convert.ToDecimal(reader["MontoCambio"]),
                                    MontoTotal = Convert.ToDecimal(reader["MontoTotal"]),
                                    oListaDetalleVenta = CD_DetalleVenta.GetListaDetalleVenta(Convert.ToInt32(reader["IdVenta"]))
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Venta>();
                    }
                }

                return lista;
            }
        }
    }
}
