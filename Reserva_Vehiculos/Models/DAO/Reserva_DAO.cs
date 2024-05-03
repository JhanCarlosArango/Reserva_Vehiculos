using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Reserva_DAO
    {
        private readonly Conexion conn;
        public List<Reserva> l_reserva;
        public List<Vehiculo> l_vehi;
        public List<Pet_reserva> l_pet;
        public Reserva _reserva;
        public Vehiculo _vehi;
        public Pet_reserva _pet;

        public Reserva_DAO()
        {
            conn = new Conexion();
        }
        public void Guardar_Reserva(int id_pet_reserva, String fk_num_placa)
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
                            var query = "insertar_reserva";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                // Define los parámetros
                                cmd.Parameters.AddWithValue("fk_id_pet_reserva", id_pet_reserva);
                                cmd.Parameters.AddWithValue("fk_num_placa", fk_num_placa);


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
                    Console.WriteLine($"Error al Guardar_Reserva: {ex.Message}");
                }

            }
        }

        public Obj_ViewModel Listar_Reservas(String usuario)
        {
            l_reserva = new List<Reserva>();
            l_vehi = new List<Vehiculo>();
            l_pet = new List<Pet_reserva>();



            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "select pr.fk_id_ubicacion_inicial,pr.fk_id_ubicacion_final,pr.fecha_ini,pr.fecha_fin,pr.hora_ini,pr.hora_fin,r.acep_fecha,r.fk_num_placa from usuario u inner join pet_reserva pr on u.id_usuario = pr.fk_id_usuario inner join reserva r ON r.fk_id_pet_reserva = pr.id_pet_reserva where u.usuario = @usuario;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario);
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _reserva = new Reserva();
                                    _vehi = new Vehiculo();
                                    _pet = new Pet_reserva();

                                    string fecha_IniString = dr["fecha_ini"].ToString();
                                    string fecha_FinString = dr["fecha_fin"].ToString();

                                    _pet.fecha_ini = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_IniString));
                                    _pet.fecha_fin = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_FinString));
                                    _pet.hora_ini = dr["hora_ini"].ToString();
                                    _pet.hora_fin = dr["hora_fin"].ToString();

                                    _reserva.acep_fecha = dr["acep_fecha"].ToString();
                                    _vehi.num_placa = dr["fk_num_placa"].ToString();

                                    l_reserva.Add(_reserva);
                                    l_vehi.Add(_vehi);
                                    l_pet.Add(_pet);
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ListarPeticion: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            var viewModel = new Obj_ViewModel
            {
                _lis_Pet_Reserva = l_pet,
                _list_vehiculos = l_vehi,
                _list_reserva = l_reserva

            };
            return viewModel;
        }

        public Obj_ViewModel Listar_Reservas_para_admin()
        {
            l_reserva = new List<Reserva>();
            l_vehi = new List<Vehiculo>();
            l_pet = new List<Pet_reserva>();



            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "select pr.fk_id_ubicacion_inicial,pr.fk_id_ubicacion_final,pr.fecha_ini,pr.fecha_fin,pr.hora_ini,pr.hora_fin,r.acep_fecha,r.fk_num_placa from usuario u inner join pet_reserva pr on u.id_usuario = pr.fk_id_usuario inner join reserva r ON r.fk_id_pet_reserva = pr.id_pet_reserva;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {

                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _reserva = new Reserva();
                                    _vehi = new Vehiculo();
                                    _pet = new Pet_reserva();

                                    //  _pet. = dr["hora_ini"].ToString(); aqui van las ubicaciones
                                    //  _pet. = dr["hora_ini"].ToString();
                                    string fecha_IniString = dr["fecha_ini"].ToString();
                                    string fecha_FinString = dr["fecha_fin"].ToString();

                                    _pet.fecha_ini = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_IniString));
                                    _pet.fecha_fin = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_FinString));

                                    _pet.hora_ini = dr["hora_ini"].ToString();
                                    _pet.hora_fin = dr["hora_fin"].ToString();

                                    _reserva.acep_fecha = dr["acep_fecha"].ToString();
                                    _vehi.num_placa = dr["fk_num_placa"].ToString();

                                    l_reserva.Add(_reserva);
                                    l_vehi.Add(_vehi);
                                    l_pet.Add(_pet);
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ListarPeticion: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            var viewModel = new Obj_ViewModel
            {
                _lis_Pet_Reserva = l_pet,
                _list_vehiculos = l_vehi,
                _list_reserva = l_reserva

            };
            return viewModel;
        }
    }
}