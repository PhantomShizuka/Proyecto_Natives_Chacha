﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Insumo
    {
        public List<Insumo> Listar()
        {
            List<Insumo> lista = new List<Insumo>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdInsumo, IdCategoria, Nombre, Descripcion, Stock, PrecioCompra, Estado from Insumo";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Insumo()
                            {
                                IdInsumo = Convert.ToInt32(reader["IdInsumo"]),
                                oCategoria = new CD_Categoria().GetCategoria(Convert.ToInt32(reader["IdCategoria"])),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"]),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    lista = new List<Insumo>();
                }
            }

            return lista;
        }
        public Insumo GetInsumo(int uIdInsumo)
        {
            return new CD_Insumo().Listar().Where(e => e.IdInsumo == uIdInsumo).FirstOrDefault();
        }
    }
}