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
    public static class CD_Permiso
    {
        public static List<Permiso> Listar
        {
            get
            {
                List<Permiso> lista = new List<Permiso>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdPermiso, IdRol, NombreMenu from Permiso";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Permiso()
                                {
                                    IdPermiso = Convert.ToInt32(reader["IdPermiso"]),
                                    IdRol = Convert.ToInt32(reader["IdRol"]),
                                    NombreMenu = reader["NombreMenu"].ToString(),
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Permiso>();
                    }
                }

                return lista;
            }
        }
        public static List<Permiso> GetListaPermiso(int uIdRol) => Listar.Where(p => p.IdRol == uIdRol).ToList();
    }
}