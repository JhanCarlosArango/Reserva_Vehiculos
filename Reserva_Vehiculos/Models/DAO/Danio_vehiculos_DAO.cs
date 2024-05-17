using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Danio_vehiculos_DAO
    {
        private readonly Conexion conn;
        public Danio_vehiculos_DAO()
        {
            conn = new Conexion();
        }
        List<Danios_vehiculos> l_danio;
        public List<Danios_vehiculos> ListarDanio()
        {
            l_danio = new List<Danios_vehiculos>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "SELECT * FROM danios_vehiculo;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Danios_vehiculos danios_Vehiculos = new Danios_vehiculos();
                                    danios_Vehiculos.id_danio = int.Parse(dr["id_danio"].ToString());
                                    danios_Vehiculos.danio = dr["danio"].ToString();
                                    danios_Vehiculos.valor_danio = Double.Parse(dr["valor_danio"].ToString());

                                    l_danio.Add(danios_Vehiculos);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al  ListarDanio: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return l_danio;
        }
    }
}