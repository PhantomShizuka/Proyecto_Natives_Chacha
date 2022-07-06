using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using(SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdUsuario, IdEmpleado, Contraseña, Estado from Usuario";
                    SqlCommand cmd = new SqlCommand(query,sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    
                    sqlConnection.Open();

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Contraseña = reader["Contraseña"].ToString(),
                                Estado= Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }

                    sqlConnection.Close();

                }catch (Exception ex)
                {
                    usuarios = new List<Usuario>();
                }
            }

            return usuarios;
        }
    }
}
