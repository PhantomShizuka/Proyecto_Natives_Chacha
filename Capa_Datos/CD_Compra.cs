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
    public static class CD_Compra
    {
        public static List<Compra> Listar
        {
            get
            {
                List<Compra> lista = new List<Compra>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdCompra, IdUsuario, IdProveedor, TipoDocumento, NmrDocumento, MontoTotal from Compra";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Compra()
                                {
                                    IdCompra = Convert.ToInt32(reader["IdCompra"]),
                                    oUsuario = CD_Usuario.GetUsuario(Convert.ToInt32(reader["IdUsuario"])),
                                    oProveedor = CD_Proveedor.GetProveedor(Convert.ToInt32(reader["IdProveedor"])),
                                    TipoDocumento = reader["TipoDocumento"].ToString(),
                                    NmrDocumento = reader["NmrDocumento"].ToString(),
                                    MontoTotal = Convert.ToDecimal(reader["MontoTotal"]),
                                    oListaDetalleCompra = CD_DetalleCompra.GetListaDetalleCompra(Convert.ToInt32(reader["IdCompra"]))
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Compra>();
                    }
                }

                return lista;
            }
        }
    }
}
