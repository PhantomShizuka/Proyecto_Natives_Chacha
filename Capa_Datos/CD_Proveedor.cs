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
    public static class CD_Proveedor
    {
        public static List<Proveedor> Listar
        {
            get
            {
                List<Proveedor> lista = new List<Proveedor>();

                using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        string query = "Select IdProveedor, Documento, RazonSocial, Telefono, Correo, Estado from Proveedor";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Proveedor()
                                {
                                    IdProveedor = Convert.ToInt32(reader["IdProveedor"]),
                                    Documento = reader["Descripcion"].ToString(),
                                    RazonSocial = reader["RazonSocial"].ToString(),
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
                        lista = new List<Proveedor>();
                    }
                }

                return lista;
            }
        }
        public static Proveedor GetProveedor(int uIdProveedor) => Listar.FirstOrDefault(e => e.IdProveedor == uIdProveedor);
    }
}
