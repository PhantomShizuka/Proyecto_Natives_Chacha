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
    public class CD_DetalleProducto
    {
        public List<DetalleProducto> Listar()
        {
            List<DetalleProducto> lista = new List<DetalleProducto>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "Select IdDetalleProducto, IdProducto, IdInsumo, Cantidad from DetalleProducto";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DetalleProducto()
                            {
                                IdDetalleProducto = Convert.ToInt32(reader["IdDetalleProducto"]),
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                oInsumo = new CD_Insumo().GetInsumo(Convert.ToInt32(reader["IdInsumo"])),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            });
                        }
                    }

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    lista = new List<DetalleProducto>();
                }
            }

            return lista;
        }
        public List<DetalleProducto> GetListaDetalleProducto(int uIdProducto)
        {
            return new CD_DetalleProducto().Listar().Where(d => d.IdProducto == uIdProducto).ToList();
        }
    }
}
