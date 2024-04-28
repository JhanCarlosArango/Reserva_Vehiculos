using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Vehiculo_DAO
    {
        private readonly Conexion conn;
        Vehiculo _vehiculo;
        List<Vehiculo> list_vehiculo;

        public Vehiculo_DAO()
        {
            conn = new Conexion();
        }

        public List<Vehiculo> ListarVehiculo()
        {
            list_vehiculo = new List<Vehiculo>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "select * from vehiculo;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                _vehiculo = new Vehiculo();
                                _vehiculo.num_placa = dr["num_placa"].ToString();
                                _vehiculo.aire_acondicionado = dr["aire_acondicionado"].ToString();
                                _vehiculo.capacidad_pasajeros = dr["capacidad_pasajeros"].ToString();
                                _vehiculo.capacidad_carga = dr["capacidad_carga"].ToString();
                                _vehiculo.fk_id_categoria = int.Parse(dr["fk_id_categoria"].ToString());
                                list_vehiculo.Add(_vehiculo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar vehiculos: {ex.Message}");
            }
            return list_vehiculo;
        }


    }
}