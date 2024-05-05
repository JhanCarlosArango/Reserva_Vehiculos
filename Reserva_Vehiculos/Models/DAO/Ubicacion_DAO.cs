using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Ubicacion_DAO
    {
        Ubicacion _ubicacion;
        List<Ubicacion> list_ubicacion;
        private readonly Conexion conn;

        public Ubicacion_DAO()
        {
            conn = new Conexion();
        }

        public List<Ubicacion> listar_ubicacion()
        {
            list_ubicacion = new List<Ubicacion>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "SELECT * FROM public.ubicacion ORDER BY id_ubicacion ASC;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                _ubicacion = new Ubicacion();
                                _ubicacion.id_ubicacion = int.Parse(dr["id_ubicacion"].ToString());
                                _ubicacion.Ubicacion_ini = dr["nombre_barrio"].ToString();
                                list_ubicacion.Add(_ubicacion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar _ubicacion {ex.Message}");
            }
            return list_ubicacion;
        }
    }
}