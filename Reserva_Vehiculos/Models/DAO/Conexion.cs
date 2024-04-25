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
            
            _connectionString = "Host=localhost;Port=5432;Database=alquiler;Username=postgres;Password=root;";
        }

        public NpgsqlConnection Conectar()
        {
            NpgsqlConnection connection = null;
            try
            {
                connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                Console.WriteLine("Conexión exitosa a PostgreSQL.");
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
            return connection;
        }
    }
}

