using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;
using Npgsql;
namespace Reserva_Vehiculos.Models.DAO
{
    public class Categoria_DAO
    {
        private readonly Conexion conn;
        List<Categoria> l_cate;
        Categoria _Categoria;
        public Categoria_DAO()
        {
            conn = new Conexion();
        }

        public List<Categoria> ListarCategoria()
        {
            l_cate = new List<Categoria>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "SELECT * FROM categoria;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Categoria cate = new Categoria();
                                    cate.id_categoria = int.Parse(dr["id_categoria"].ToString());
                                    cate.tipo_vehiculo = dr["tipo_vehiculo"].ToString();
                                    cate.costo = Double.Parse(dr["costo"].ToString());

                                    l_cate.Add(cate);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar usuarios: {ex.Message}");
                }
            } else
            {
                Console.WriteLine("Conn Null");
            }
            return l_cate;
        }

        public Categoria ListarCategoria(String nomre)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "select ca.id_marca from marca ca where nombre_marca = @nomre;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@nomre", nomre);
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _Categoria = new Categoria();
                                    _Categoria.id_categoria = int.Parse(dr["id_categoria"].ToString());
                                    _Categoria.tipo_vehiculo = dr["tipo_vehiculo"].ToString();


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar ListarListarCategoria: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return _Categoria;
        }
    }
    }
