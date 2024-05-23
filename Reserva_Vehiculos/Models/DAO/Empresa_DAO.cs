using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Reserva_Vehiculos.Models;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Empresa_DAO
    {
        private readonly Conexion conn;
        Empresa empre;

        public Empresa_DAO()
        {
            conn = new Conexion();
        }

        public Empresa traer_empresa()
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = @"
                    SELECT e.nit,
                    e.nombre_empresa,
                    e.email,
                    e.direccion,
                    e.telefono,
                    e.logo
                    FROM empresa e
                    ORDER BY nit ASC  LIMIT 1;
                    ";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                empre = new Empresa();
                                empre.nit = int.Parse(dr["nit"].ToString());
                                empre.name_empresa = dr["nombre_empresa"].ToString();
                                empre.correo_em = dr["email"].ToString();
                                empre.direccion = dr["direccion"].ToString();
                                empre.logo = dr["logo"].ToString();
                                empre.telefono = dr["telefono"].ToString();

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
            return empre;
        }
    }
}
