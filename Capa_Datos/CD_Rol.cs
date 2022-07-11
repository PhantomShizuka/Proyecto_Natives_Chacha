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
    public static class CD_Rol
    {
        public static List<Rol> Listar
        {
            get
            {
                List<Rol> lista = new List<Rol>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdRol, Descripcion from Rol";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Rol()
                                {
                                    IdRol = Convert.ToInt32(reader["IdRol"]),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    oListaPermiso = CD_Permiso.GetListaPermiso(Convert.ToInt32(reader["IdRol"]))
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Rol>();
                    }
                }

                return lista;
            }
        }
        public static Rol GetRol(int uIdRol) => Listar.FirstOrDefault(r => r.IdRol == uIdRol);
    }
}
