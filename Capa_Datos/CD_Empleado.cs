﻿using System;
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
        public static Empleado GetEmpleado(int uIdEmpleado) => Listar.FirstOrDefault(e => e.IdEmpleado == uIdEmpleado);
    }
}
