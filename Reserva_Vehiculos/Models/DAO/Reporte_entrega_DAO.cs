using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Reporte_entrega_DAO
    {
        private readonly Conexion conn;
        public Reporte_entrega_DAO()
        {
            conn = new Conexion();
        }
        public int Get_id_reporte_entrega()
        {
            int id = 0;
            using (var connection = conn.Conectar())
            {
                try
                {
                    // Consulta para obtener los roles del usuario
                    string query = "select repo_en.id_reporte from reporte_entrega repo_en order by repo_en.id_reporte DESC LIMIT 1";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                id = int.Parse(dr["id_reporte"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener rol: {ex.Message}");
                }
            }

            return id;
        }
        public void Guardar_Reporte_entrega(DateOnly fecha_entrega, String hora_entrega, int fk_id_ubicacion, string fk_num_placa)
        {

            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {
                        if (connection != null)
                        {
                            var query = @"INSERT INTO reporte_entrega(fecha_entrega, hora_entrega, fk_num_placa,fk_id_ubicacion)
	                                    VALUES (@fecha_entrega, @hora_entrega,@fk_num_placa ,@fk_id_categoria);";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@fecha_entrega", fecha_entrega);
                                cmd.Parameters.AddWithValue("@hora_entrega", hora_entrega);
                                cmd.Parameters.AddWithValue("@fk_num_placa", fk_num_placa);
                                cmd.Parameters.AddWithValue("@fk_id_categoria", fk_id_ubicacion);
                                cmd.CommandType = CommandType.Text; // Establece el tipo de comando como texto
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
                    Console.WriteLine($"Error al Guardar_Reporte_entrega: {ex.Message}");
                }

            }
        }
        public void Guardar_Itermedia_reporte_danio(int fk_id_danio, int fk_id_reporte)
        {

            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {
                        if (connection != null)
                        {
                            int c = 1;
                            var query = @"INSERT INTO reporte_danio(fk_id_danio, fk_id_reporte)VALUES (@fk_id_danio, @fk_id_reporte);";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@fk_id_danio", fk_id_danio);
                                cmd.Parameters.AddWithValue("@fk_id_reporte", fk_id_reporte);
                                cmd.CommandType = CommandType.Text; // Establece el tipo de comando como texto
                                cmd.ExecuteNonQuery();

                                Console.WriteLine("Datos insertados correctamente." + c);
                                c++;
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
                    Console.WriteLine($"Error al Guardar_Itermedia_reporte_danio: {ex.Message}");
                }

            }
        }
    }
}


