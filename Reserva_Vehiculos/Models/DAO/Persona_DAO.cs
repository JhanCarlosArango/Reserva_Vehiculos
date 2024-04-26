using System;
using System.Collections.Generic;
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
                    var query = "select  pe.num_documento,pe.primer_nombre,pe.segundo_nombre,pe.primer_apellido,pe.segundo_apellido,pe.num_telefonico from Persona pe inner join usuario u ON u.fk_num_documento = pe.num_documento where  u.usuario = @usuario;";  // corregir, llamar un vista 
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
    }
}