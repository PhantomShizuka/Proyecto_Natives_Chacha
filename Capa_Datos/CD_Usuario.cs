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
    public static class CD_Usuario
    {
        public static List<Usuario> Listar
        {
            get
            {
                List<Usuario> lista = new List<Usuario>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdUsuario, IdEmpleado, IdRol, Contraseña, Estado from Usuario";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                    oEmpleado = CD_Empleado.GetEmpleado(Convert.ToInt32(reader["IdEmpleado"])),
                                    oRol = CD_Rol.GetRol(Convert.ToInt32(reader["IdRol"])),
                                    Contraseña = reader["Contraseña"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"])
                                });
                            }
                        }

                        sqlConnection.Close();

                    }
                    catch (Exception)
                    {
                        lista = new List<Usuario>();
                    }
                }

                return lista;
            }
        }

        public static int Registrar(Usuario obj, out string Mensaje)
        {
            int idgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", sqlConnection) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.oEmpleado.IdEmpleado);                    
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Contraseña", obj.Contraseña);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    idgenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                idgenerado = 0;
                Mensaje = ex.Message;
            }

            return idgenerado;
        }

        public static bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", sqlConnection);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Contraseña", obj.Contraseña);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        public static bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", sqlConnection);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        public static Usuario GetUsuario(int uIdUsuario) => Listar.FirstOrDefault(u => u.IdUsuario == uIdUsuario);
    }
}
