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
            List<Usuario> lista = new List<Usuario>();

            using(SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdUsuario, IdEmpleado, IdRol, Contraseña, Estado from Usuario";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    
                    sqlConnection.Open();

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                oEmpleado = new CD_Empleado().GetEmpleado(Convert.ToInt32(reader["IdEmpleado"])),
                                oRol = new CD_Rol().GetRol(Convert.ToInt32(reader["IdRol"])),
                                Contraseña = reader["Contraseña"].ToString(),
                                Estado= Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }

                    sqlConnection.Close();

                }catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }

            return lista;
        }
        public Usuario GetUsuario(int uIdUsuario)
        {
            return new CD_Usuario().Listar().Where(u => u.IdUsuario == uIdUsuario).FirstOrDefault();
        }
    }
}
