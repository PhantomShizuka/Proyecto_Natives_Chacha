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
    public static class CD_Cliente
    {
        public static List<Cliente> Listar
        {
            get
            {
                List<Cliente> lista = new List<Cliente>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdCliente, Documento, NombreCompleto, Telefono, Correo, Estado from Cliente";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                    Documento = reader["Descripcion"].ToString(),
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
                        lista = new List<Cliente>();
                    }
                }

                return lista;
            }
        }
    }
}
