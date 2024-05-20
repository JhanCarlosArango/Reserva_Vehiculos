using System;
using System.Collections.Generic;
using System.Data;
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

        public void Guardar_Especificcacio_Vehiculos(String modelo, String color, String num_chasis, String modelo_motor, String cilindraje, int fk_id_tipo_combustible)
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
                            var query = @"INSERT INTO espec_vehiculo(
                    modelo, color, num_chasis, modelo_motor, cilindraje, fk_id_tipo_combustible)
                    VALUES (@modelo, @color, @num_chasis, @modelo_motor, @cilindraje, @fk_id_tipo_combustible);";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {
                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@modelo", modelo);
                                cmd.Parameters.AddWithValue("@color", color);
                                cmd.Parameters.AddWithValue("@num_chasis", num_chasis);
                                cmd.Parameters.AddWithValue("@modelo_motor", modelo_motor);
                                cmd.Parameters.AddWithValue("@cilindraje", cilindraje);
                                cmd.Parameters.AddWithValue("@fk_id_tipo_combustible", fk_id_tipo_combustible);

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
                    Console.WriteLine($"Error al Guardar_Especificcacio_Vehiculos: {ex.Message}");
                }

            }
        }
        public void Guardar_Vehiculos(String num_placa, int capacidad_pasajeros, String capacidad_carga, int fk_id_categoria, int fk_id_tipo_direccion, int fk_id_caja_cambios, int fk_id_marca, String fk_num_chasis)
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
                            var query = @"INSERT INTO public.vehiculo(
	                                    num_placa, capacidad_pasajeros, capacidad_carga, fk_id_categoria, fk_id_tipo_direccion, fk_id_caja_cambios, fk_id_marca, fk_num_chasis)
	                                    VALUES (@num_placa, @capacidad_pasajeros, @capacidad_carga, @fk_id_categoria, @fk_id_tipo_direccion, @fk_id_caja_cambios, @fk_id_marca, @fk_num_chasis);";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {


                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@num_placa", num_placa);
                                cmd.Parameters.AddWithValue("@capacidad_pasajeros", capacidad_pasajeros);
                                cmd.Parameters.AddWithValue("@capacidad_carga", capacidad_carga);
                                cmd.Parameters.AddWithValue("@fk_id_categoria", fk_id_categoria);
                                cmd.Parameters.AddWithValue("@fk_id_tipo_direccion", fk_id_tipo_direccion);
                                cmd.Parameters.AddWithValue("@fk_id_caja_cambios", fk_id_caja_cambios);
                                cmd.Parameters.AddWithValue("@fk_id_marca", fk_id_marca);
                                cmd.Parameters.AddWithValue("@fk_num_chasis", fk_num_chasis);

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

        public List<Vehiculo> ListarVehiculo_Disponibles(int fk_id_categoria, DateOnly fecha_ini, DateOnly fecha_fin)
        {
            string consulta = @"
                        SELECT *
                        FROM (
                            SELECT v.num_placa, v.fk_id_categoria
                            FROM vehiculo v
                            WHERE v.num_placa NOT IN (
                                SELECT r.fk_num_placa
                                FROM reserva r
                                INNER JOIN pet_reserva pr ON pr.id_pet_reserva = r.fk_id_pet_reserva
                                WHERE r.estado_reserva = 'activa' AND (
                                    (pr.fecha_ini >= @fecha_ini AND pr.fecha_fin <= @fecha_fin) OR
                                    (pr.fecha_ini <= @fecha_ini AND pr.fecha_fin >= @fecha_fin) OR
                                    (pr.fecha_ini BETWEEN @fecha_ini AND @fecha_fin) OR
                                    (pr.fecha_fin BETWEEN @fecha_ini AND @fecha_fin)
                                )
                            )
                        ) AS subconsulta
                        WHERE subconsulta.fk_id_categoria = @fk_id_categoria;";
            list_vehiculo = new List<Vehiculo>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = consulta;  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fk_id_categoria", fk_id_categoria);
                        cmd.Parameters.AddWithValue("@fecha_ini", fecha_ini);
                        cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                _vehiculo = new Vehiculo();
                                _vehiculo.num_placa = dr["num_placa"].ToString();
                                _vehiculo.fk_id_categoria = int.Parse(dr["fk_id_categoria"].ToString());
                                list_vehiculo.Add(_vehiculo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar vehiculos disponibles: {ex.Message}");
            }
            return list_vehiculo;
        }
        public Vehiculo BUscarVehiculo(String placa)
        {

            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = @"
                    select 
                    re.id_reserva,
                    v.num_placa,
                    v.capacidad_pasajeros,
                    v.capacidad_carga,
                    v.estado,
                    es.modelo,
                    es.color,
                    es.num_chasis,
                    es.modelo_motor,
                    es.cilindraje,
                    mr.nombre_marca,
                    cate.tipo_vehiculo,
                    cc.tipo_caja_cambios,
                    td.tipo_direccion,
                    tc.tipo_combustible,
                    pet.costo as costo_reserva,
                    pet.fecha_ini,
                    pet.fecha_fin,
                    pet.hora_ini,
                    pet.hora_fin,
                    pers.primer_apellido || ' ' || pers.segundo_apellido AS nombre_completo,
                    pers.num_documento
                    from vehiculo v 
                    inner join espec_vehiculo es ON es.num_chasis = v.fk_num_chasis
                    inner join marca mr on mr.id_marca = v.fk_id_marca
                    inner join categoria cate on cate.id_categoria = v.fk_id_categoria
                    inner join caja_cambios cc ON cc.id_caja_cambios = v.fk_id_caja_cambios
                    inner join tipo_direccion td on td.id_tipo_direccion = v.fk_id_tipo_direccion
                    inner join tipo_combustible  tc ON tc.id_tipo_combustible = es.fk_id_tipo_combustible
                    inner join reserva re on re.fk_num_placa = v.num_placa
                    inner join pet_reserva pet on pet.id_pet_reserva = re.fk_id_pet_reserva
                    inner join usuario u on u.id_usuario = pet.fk_id_usuario
                    inner join persona pers on pers.num_documento = u.fk_num_documento
                    where re.fk_num_placa = @placa;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@placa", placa);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                _vehiculo = new Vehiculo();
                                _vehiculo.id_reserva_temp = int.Parse(dr["id_reserva"].ToString());
                                _vehiculo.num_placa = dr["num_placa"].ToString();
                                _vehiculo.modelo = dr["modelo"].ToString();
                                _vehiculo.num_chasis = dr["num_chasis"].ToString();
                                _vehiculo.modelo_motor = dr["modelo_motor"].ToString();
                                _vehiculo.cilindraje = dr["cilindraje"].ToString();
                                _vehiculo.tipo_vehiculo = dr["tipo_vehiculo"].ToString();
                                //_vehiculo.fk_id_categoria = int.Parse(dr["fk_id_categoria"].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al BUscarVehiculo: {ex.Message}");
            }
            return _vehiculo;
        }
    }
}
