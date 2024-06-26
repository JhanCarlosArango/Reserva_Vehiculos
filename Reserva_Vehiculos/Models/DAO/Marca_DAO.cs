using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Marca_DAO
    {

        private readonly Conexion conn;
        List<Marca> l_marca;
        Marca _marca;
        public Marca_DAO()
        {
            conn = new Conexion();
        }

        public List<Marca> ListarMarca()
        {
            l_marca = new List<Marca>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "SELECT * FROM marca;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Marca _marca = new Marca();
                                    _marca.id_marca = int.Parse(dr["id_marca"].ToString());
                                    _marca.nombre_marca = dr["nombre_marca"].ToString();

                                    l_marca.Add(_marca);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar ListarMarca: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return l_marca;
        }

        public Marca ListarMarca(String nomre)
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
                                    _marca = new Marca();
                                    _marca.id_marca = int.Parse(dr["id_marca"].ToString());
                                    _marca.nombre_marca = dr["nombre_marca"].ToString();


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar ListarListarMarca: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return _marca;
        }
    }
}
