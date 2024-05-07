using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using System.Configuration;
using Npgsql;
using System.Data;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Usuario_DAO
    {
        private readonly Conexion conn;
        Usuarios user;

        public Usuario_DAO()
        {
            conn = new Conexion();
        }

        public List<Rol> GetRoles(int id)
        {
            List<Rol> roles = new List<Rol>();
            using (var connection = conn.Conectar())
            {
                try
                {
                    // Consulta para obtener los roles del usuario
                    string query = "SELECT r.id_rol FROM usuario_rol ur JOIN rol r ON ur.fk_id_rol = r.id_rol join usuario u on ur.fk_id_usuario = u.id_usuario WHERE ur.fk_id_usuario = @id";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                String id_Rol = dr["id_rol"].ToString();
                                Rol rol = (Rol)Enum.Parse(typeof(Rol), id_Rol);
                                roles.Add(rol);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener rol: {ex.Message}");
                }
            }

            return roles;
        }

        public Usuarios Search_user(String use, String pass)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "SELECT * from usuario  WHERE usuario = @use and contrasenia = @pass;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@use", use);
                        cmd.Parameters.AddWithValue("@pass", pass);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["usuario"] != null)
                                {
                                    user = new Usuarios();
                                    user.id_user = int.Parse(dr["id_usuario"].ToString());
                                    user.usuario = dr["usuario"].ToString();
                                    user.contrasenia = dr["contrasenia"].ToString();
                                    user.rols = GetRoles(user.id_user);
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar usuarios: {ex.Message}");
            }
            return user;
        }

        public int Obtener_ID_usuario(String use)
        {
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo
            try
            {
                using (connection)
                {
                    var query = "select u.id_usuario from usuario u where u.usuario = @use;";  // corregir, llamar un vista 
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@use", use);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                user = new Usuarios();
                                user.id_user = int.Parse(dr["id_usuario"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar usuarios: {ex.Message}");
            }
            return user.id_user;
        }
        public void Guardar_usuario(int fk_num_documento, String usuario, String contrasenia)
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
                            var query = @"INSERT INTO usuario(usuario, contrasenia, fk_num_documento) VALUES (@usuario, @contrasenia, @fk_num_documento);";
                            using (var cmd = new NpgsqlCommand(query, connection))
                            {


                                // Define los parámetros
                                cmd.Parameters.AddWithValue("@usuario", usuario);
                                cmd.Parameters.AddWithValue("@contrasenia", contrasenia);
                                cmd.Parameters.AddWithValue("@fk_num_documento", fk_num_documento);


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
                    Console.WriteLine($"Error al Guardar_usuario: {ex.Message}");
                }

            }
        }
    }

}
