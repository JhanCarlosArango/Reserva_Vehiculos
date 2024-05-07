using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Tipo_Direccion_DAO
    {
        private readonly Conexion conn;

        Tipo_Direccion Tipo_Direccion_;

        List<Tipo_Direccion> list_dire;
        public Tipo_Direccion_DAO()
        {
            conn = new Conexion();
        }

        public Tipo_Direccion Listartipodireccion(String tipo_direccion)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "SELECT tp.id_tipo_direccion, tp.tipo_direccion FROM tipo_direccion tp WHERE tp.tipo_direccion = @tipo_direccion;";

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@tipo_direccion", tipo_direccion);
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Tipo_Direccion_ = new Tipo_Direccion();
                                    Tipo_Direccion_.id_tipo_direccion = int.Parse(dr["id_tipo_direccion"].ToString());
                                    Tipo_Direccion_.tipo_direccion = dr["tipo_direccion"].ToString();


                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar Listartipodireccion: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return Tipo_Direccion_;
        }

        public List<Tipo_Direccion> ListarCTipo_Direccion()
        {
            list_dire = new List<Tipo_Direccion>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "select * from tipo_direccion;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                Tipo_Direccion_ = new Tipo_Direccion();
                                Tipo_Direccion_.id_tipo_direccion =  int.Parse(dr["id_tipo_direccion"].ToString());
                                Tipo_Direccion_.tipo_direccion = dr["tipo_direccion"].ToString();
                                list_dire.Add(Tipo_Direccion_);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar ListarCTipo_Direccion: {ex.Message}");
            }
            return list_dire;
        }

    }
}
