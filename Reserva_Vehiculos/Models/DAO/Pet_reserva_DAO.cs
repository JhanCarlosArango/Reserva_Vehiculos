using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;
using Npgsql;
namespace Reserva_Vehiculos.Models.DAO
{
    public class Pet_reserva_DAO
    {
        List<Pet_reserva> list_pet;
        private readonly Conexion conn;

        public Pet_reserva_DAO()
        {
            conn = new Conexion();
        }
        public List<Pet_reserva> ListarPeticion()
        {
            list_pet = new List<Pet_reserva>();
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
                                    Pet_reserva _pet = new Pet_reserva();
                                    // _pet.tipo_vehiculo = dr["tipo_vehiculo"].ToString();
                                    // _pet.costo = Double.Parse(dr["costo"].ToString());

                                    list_pet.Add(_pet);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar usuarios: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return list_pet;
        }
    }
}