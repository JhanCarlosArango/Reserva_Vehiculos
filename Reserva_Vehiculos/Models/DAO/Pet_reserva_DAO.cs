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
        Reserva _reserva;
        Pet_reserva _pet;
        Ubicacion _ubi;
        Categoria _cate;
        Usuarios _user;
        Persona _per;

        private readonly Conexion conn;

        public Pet_reserva_DAO()
        {
            conn = new Conexion();
        }
        public int Traer_ID_ubicacion(int ID_ubicacion)
        {

            return ID_ubicacion;
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

                        var query = "SELECT * FROM pet_reserva where estado = 'wait' ORDER BY id_pet_reserva ASC;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Pet_reserva _pet = new Pet_reserva();
                                    _pet.id_pet_reserva = int.Parse(dr["id_pet_reserva"].ToString());
                                    string fecha_IniString = dr["fecha_ini"].ToString();
                                    string fecha_FinString = dr["fecha_fin"].ToString();

                                    _pet.fecha_ini = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_IniString));
                                    _pet.fecha_fin = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_FinString));

                                    _pet.hora_ini = dr["hora_ini"].ToString();
                                    _pet.hora_fin = dr["hora_fin"].ToString();
                                    _pet.fk_id_categoria = int.Parse(dr["fk_id_categoria"].ToString());
                                    _pet.fk_id_usuario = int.Parse(dr["fk_id_usuario"].ToString());
                                    //_pet.ubicacion_inicial = int.Parse(dr["fk_id_ubicacion_inicial"].ToString()); //Ubicacion
                                    //_pet.ubicacion_final = int.Parse(dr["fk_id_ubicacion_final"].ToString());
                                    list_pet.Add(_pet);
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
            return list_pet;
        }

        public void Guardar_Pet_reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, int fk_id_ubicacion_inicial, int fk_id_ubicacion_final, int fk_id_categoria, int fk_id_usuario)
        {
            // list_pet = new List<Pet_reserva>();
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
                                cmd.Parameters.AddWithValue("fk_id_ubicacion_ini", fk_id_ubicacion_inicial);
                                cmd.Parameters.AddWithValue("fk_id_ubicacion_fin", fk_id_ubicacion_final);
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

        public void Actualiza_estado(int id_pet_reserva)
        {
            int estado = -1;
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {
                        if (connection != null)
                        {
                            var query = "SELECT sp_estado_pet_reserva(@id_pet_reserva)";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                cmd.CommandType = CommandType.Text;

                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@id_pet_reserva", id_pet_reserva);


                                object result = cmd.ExecuteScalar();

                                // Si el resultado no es nulo, conviértelo al tipo de datos correcto
                                if (result != DBNull.Value && result != null)
                                {
                                    estado = Convert.ToInt32(result);
                                }

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
                    Console.WriteLine($"Error al Actualiza_estado: {ex.Message}");
                }

            }
        }
        public void CANCELAR_PET_RESERVA(String fk_num_placa)
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
                            var query = @"update pet_reserva  set estado =  'desca' where fk_num_placa = @fk_num_placa;";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {


                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@fk_num_placa", fk_num_placa);


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
                    Console.WriteLine($"Error al Guardar_Vehiculos: {ex.Message}");
                }

            }
        }


        public Obj_ViewModel Listar_PDF_Pet_Reservas(String usuario)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {
                        var query = @"
                                        SELECT
                                            pr.id_pet_reserva,
                                            per.primer_apellido || ' ' || per.segundo_apellido AS nombre_completo,
                                            pr.fecha_ini,
                                            pr.fecha_fin,
                                            pr.hora_ini,
                                            pr.hora_fin,
                                            ubi_ini.nombre_barrio AS Recojida,
                                            ubi_fin.nombre_barrio AS Devolucion,
                                            cate.tipo_vehiculo
                                        FROM
                                            pet_reserva pr
                                        INNER JOIN
                                            ubicacion ubi_ini ON ubi_ini.id_ubicacion = pr.fk_id_ubicacion_ini
                                        INNER JOIN
                                            ubicacion ubi_fin ON ubi_fin.id_ubicacion = pr.fk_id_ubicacion_fin
                                        INNER JOIN
                                            categoria cate ON cate.id_categoria = pr.fk_id_categoria
                                        INNER JOIN
                                            usuario u ON u.id_usuario = pr.fk_id_usuario
                                        INNER JOIN
                                            persona per ON per.num_documento = u.fk_num_documento
                                        WHERE
                                            u.usuario = @usuario
                                        ORDER BY
                                            pr.id_pet_reserva DESC
                                        LIMIT 1;";  // corregir, llamar un vista 

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario);
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    _pet = new Pet_reserva();
                                    _ubi = new Ubicacion();
                                    _cate = new Categoria();
                                    _user = new Usuarios();
                                    _per = new Persona();

                                    _per.f_name = dr["nombre_completo"].ToString();

                                    string fecha_IniString = dr["fecha_ini"].ToString();
                                    string fecha_FinString = dr["fecha_fin"].ToString();

                                    _pet.fecha_ini = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_IniString));
                                    _pet.fecha_fin = DateOnly.Parse(_pet.ObtenerPrimeraParteSeparadaPorEspacio(fecha_FinString));
                                    _pet.hora_ini = dr["hora_ini"].ToString();
                                    _pet.hora_fin = dr["hora_fin"].ToString();
                                    _ubi.Ubicacion_ini = dr["Recojida"].ToString();
                                    _ubi.ubicacion_fin = dr["Devolucion"].ToString();
                                    _cate.tipo_vehiculo = dr["tipo_vehiculo"].ToString();
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al Listar_PDF_Pet_Reservas : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            var viewModel = new Obj_ViewModel
            {
                _Pet_Reserva = _pet,
                _ubi_ = _ubi,
                categoria = _cate,
                _user_ = _user,
                _persona = _per
            };
            return viewModel;
        }
    }
}