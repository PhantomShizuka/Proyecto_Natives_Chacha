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
    public static class CD_Empleado
    {
        public static List<Empleado> Listar
        {
            get
            {
                List<Empleado> lista = new List<Empleado>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdEmpleado, Documento, NombreCompleto, Telefono, Correo, Estado from Empleado"; //Gente escribam bien, por escribir Emplado sufri bastante
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Empleado()
                                {
                                    IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                    Documento = reader["Documento"].ToString(),
                                    NombreCompleto = reader["NombreCompleto"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"])
                                });
                            }
                        }

                        sqlConnection.Close();
                    }
                    catch (Exception)
                    {
                        lista = new List<Empleado>();
                    }
                }

                return lista;
            }
        }
        public static int Registrar(Empleado obj, out string Mensaje)
        {
            int IdResultado = 0;
            Mensaje = string.Empty;
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAREMPLEADO", sqlConnection) { CommandType = CommandType.StoredProcedure };
                    
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    IdResultado = Convert.ToInt32(cmd.Parameters["IdResultado"].Value);  
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                IdResultado = 0;
                Mensaje = ex.Message;
            }

            return IdResultado;
        }
        public static bool Editar(Empleado obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITAREMPLEADO", sqlConnection) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.IdEmpleado);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

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
        public static bool Eliminar(Empleado obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAREMPLEADO", sqlConnection);
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.IdEmpleado);
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
        public static Empleado GetEmpleado(int uIdEmpleado) => Listar.FirstOrDefault(e => e.IdEmpleado == uIdEmpleado);
    }
}
