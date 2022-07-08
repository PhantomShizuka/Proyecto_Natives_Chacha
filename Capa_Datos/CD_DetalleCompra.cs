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
    public class CD_DetalleCompra
    {
        public List<DetalleCompra> Listar()
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdDetalleCompra, IdCompra, IdInsumo, PrecioCompra, Cantidad, SubTotal from DetalleCompra";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DetalleCompra()
                            {
                                IdDetalleCompra = Convert.ToInt32(reader["IdDetalleCompra"]),
                                IdCompra = Convert.ToInt32(reader["IdCompra"]),
                                oInsumo = new CD_Insumo().GetInsumo(Convert.ToInt32(reader["IdInsumo"])),
                                PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"])
                            });
                        }
                    }

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    lista = new List<DetalleCompra>();
                }
            }

            return lista;
        }
        public List<DetalleCompra> GetListaDetalleCompra(int uIdCompra)
        {
            return new CD_DetalleCompra().Listar().Where(d => d.IdCompra == uIdCompra).ToList();
        }
    }
}
