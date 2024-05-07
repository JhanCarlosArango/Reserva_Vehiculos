using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Reserva_Vehiculos.Models;
namespace Reserva_Vehiculos.Models.DAO
{
    public class Caja_Cambio_DAO
    {
        private readonly Conexion conn;

        Caja_Cambio _caja;
        List<Caja_Cambio> lista_caja;

        public Caja_Cambio_DAO()
        {
            conn = new Conexion();
        }

        public Caja_Cambio ListarCajas(String nomre)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "select ca.tipo_caja_cambios from caja_cambios ca where id_caja_cambios = nomre;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@nomre", nomre);
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _caja = new Caja_Cambio();
                                    _caja.id_caja_cambios = int.Parse(dr["id_caja_cambios"].ToString());
                                    _caja.tipo_caja_cambios = dr["tipo_caja_cambios"].ToString();


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar ListarCajas: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return _caja;
        }

        public List<Caja_Cambio> ListarCajas()
        {

            lista_caja = new List<Caja_Cambio>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "select * from caja_cambios;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {

                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _caja = new Caja_Cambio();
                                    _caja.id_caja_cambios = int.Parse(dr["id_caja_cambios"].ToString());
                                    _caja.tipo_caja_cambios = dr["tipo_caja_cambios"].ToString();
                                    lista_caja.Add(_caja);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar ListarCajas: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return lista_caja;
        }
    }
}
