using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;
using Npgsql;
using System.Data;
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

                        var query = "";  // corregir, llamar un vista 
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

        public void Guardar_Pet_reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, int fk_id_categoria, int fk_id_usuario)
        {
            list_pet = new List<Pet_reserva>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {
                        if (connection != null)
                        {
                            var query = "insertar_datos_pet";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                // Define los parámetros
                                cmd.Parameters.AddWithValue("fecha_ini", fecha_ini);
                                cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
                                cmd.Parameters.AddWithValue("hora_ini", hora_ini);
                                cmd.Parameters.AddWithValue("hora_fin", hora_fin);
                                cmd.Parameters.AddWithValue("fk_id_categoria", fk_id_categoria);
                                cmd.Parameters.AddWithValue("fk_id_usuario", fk_id_usuario);

                                // Ejecuta el procedimiento almacenado
                                cmd.ExecuteNonQuery();

                                Console.WriteLine("Datos insertados correctamente.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("La conexión es nula.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar Pet_Reserva: {ex.Message}");
                }

            }
        }
    }
}