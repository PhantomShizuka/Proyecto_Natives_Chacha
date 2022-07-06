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
    public class CD_Empleado
    {
        public List<Empleado> Listar()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdEmpleado, Documento, NombreCompleto, Telefono, Correo, Estado from Emplado";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text }; 
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            empleados.Add(new Empleado()
                            {
                                IdEmpleado = Convert.ToInt32(reader["IdEmplado"]),
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
                    empleados = new List<Empleado>();
                }
            }

            return empleados;
        }
    }
}
