using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;
using Npgsql;
namespace Reserva_Vehiculos.Models.DAO
{
    public class Conexion
    {
        private readonly string _connectionString;

        public Conexion()
        {
            // Configura la cadena de conexión con tus propios valores
            _connectionString = "Host=reservas.chea08gwkn1d.us-east-1.rds.amazonaws.com;Port=5432;Database=reserva;Username=jkgamer;Password=01760091;";
        }

        public void Conectar()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a PostgreSQL.");
                    // Realiza operaciones con la base de datos aquí
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
        }
    }
}

