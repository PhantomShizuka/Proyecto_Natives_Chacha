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
    public static class CD_Promocion
    {
        public static List<Promocion> Listar
        {
            get
            {
                List<Promocion> lista = new List<Promocion>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdPromocion, DescuentoFijo, DescuentoPorcentual, Estado, TipoDescuento from Promocion";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Promocion()
                                {
                                    IdPromocion = Convert.ToInt32(reader["IdPromocion"]),
                                    DescuentoFijo = Convert.ToInt32(reader["DescuentoFijo"]),
                                    DescuentoPorcentual = Convert.ToDecimal(reader["DescuentoPorcentual"]),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    TipoDescuento = Convert.ToBoolean(reader["TipoDescuento"])
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Promocion>();
                    }
                }

                return lista;
            }
        }
        public static Promocion GetPromocion(int uIdPromocion) => Listar.FirstOrDefault(p => p.IdPromocion == uIdPromocion);
    }
}
