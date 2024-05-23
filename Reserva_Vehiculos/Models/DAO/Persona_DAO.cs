using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{

    public class Persona_DAO
    {
        Persona person;
        private readonly Conexion conn;

        public Persona_DAO()
        {
            conn = new Conexion();
        }
        public Persona Search_persona(String usuario)
        {
            //String usuario ="arango";
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "select  pe.num_documento,pe.primer_nombre,pe.segundo_nombre,pe.primer_apellido,pe.segundo_apellido,pe.num_telefonico,pe.correo from Persona pe inner join usuario u ON u.fk_num_documento = pe.num_documento where  u.usuario = @usuario;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                //if (dr["usuario"] != null)
                                // {// falta tipo docu
                                person = new Persona();
                                person.f_name = dr["primer_nombre"].ToString();
                                person.s_name = dr["segundo_nombre"].ToString();
                                person.f_lastname = dr["primer_apellido"].ToString();
                                person.s_lastname = dr["segundo_apellido"].ToString();
                                person.num_telefonico = dr["num_telefonico"].ToString();
                                person.corre = dr["correo"].ToString();

                                // }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar Persona: {ex.Message}");
            }
            return person;
        }
        public void Guardar_Persona(int num_documento, String primer_nombre, String segundo_nombre, String primer_apellido, String segundo_apellido, String num_telefonico, int fk_id_tipo_doc, String correo)
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
                           var query = @"INSERT INTO public.persona(
                                num_documento, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, num_telefonico, fk_id_tipo_doc, correo)
                                VALUES (@num_documento, @primer_nombre, @segundo_nombre, @primer_apellido, @segundo_apellido, @num_telefonico, @fk_id_tipo_doc, @correo);";

                            using (var cmd = new NpgsqlCommand(query, connection))
                            {


                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@num_documento", num_documento);
                                cmd.Parameters.AddWithValue("@primer_nombre", primer_nombre);
                                cmd.Parameters.AddWithValue("@segundo_nombre", segundo_nombre);
                                cmd.Parameters.AddWithValue("@primer_apellido", primer_apellido);
                                cmd.Parameters.AddWithValue("@segundo_apellido", segundo_apellido);
                                cmd.Parameters.AddWithValue("@num_telefonico", num_telefonico);
                                cmd.Parameters.AddWithValue("@fk_id_tipo_doc", fk_id_tipo_doc);
                                cmd.Parameters.AddWithValue("@correo", correo);

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
                    Console.WriteLine($"Error al Guardar_Persona: {ex.Message}");
                }

            }
        }
    }
}