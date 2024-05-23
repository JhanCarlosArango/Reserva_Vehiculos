using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;
using Npgsql;
namespace Reserva_Vehiculos.Models.DAO
{
    public class Categoria_DAO
    {
        private readonly Conexion conn;
        List<Categoria> l_cate;
        //Categoria _Categoria;
        public Categoria_DAO()
        {
            conn = new Conexion();
        }

        public List<Categoria> ListarCategoria()
        {
            l_cate = new List<Categoria>();
            var connection = conn.Conectar(); //  es posible mejorar esta linea de codigo

            if (connection != null)
            {
                try
                {
                    using (connection)
                    {

                        var query = "SELECT * FROM categoria;";  // corregir, llamar un vista 
                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Categoria cate = new Categoria();
                                    cate.id_categoria = int.Parse(dr["id_categoria"].ToString());
                                    cate.tipo_vehiculo = dr["tipo_vehiculo"].ToString();
                                    cate.costo = Double.Parse(dr["costo"].ToString());

                                    var rutaImagen = ObtenerRutaImagenCategoria(cate.id_categoria);
                                    var rutaAbsoluta = ObtenerRutaAbsoluta(rutaImagen);
                                    byte[] imagenBytes = null;
                                    if (!string.IsNullOrEmpty(rutaAbsoluta) && System.IO.File.Exists(rutaAbsoluta))
                                    {
                                        imagenBytes = System.IO.File.ReadAllBytes(rutaAbsoluta);
                                    }

                                    cate.Imagen = imagenBytes;
                                    cate.RutaImagen = rutaImagen;
                                    l_cate.Add(cate);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar usuarios: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Conn Null");
            }
            return l_cate;
        }
        private string ObtenerRutaImagenCategoria(int idCategoria)
        {
            var connection = conn.Conectar();
            if (connection != null)
            {
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT ruta_imagen FROM categoria WHERE id_categoria = @IdCategoria";
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }

        // M�todo para obtener la ruta absoluta a partir de la ruta relativa
        private string ObtenerRutaAbsoluta(string rutaRelativa)
        {
            if (string.IsNullOrEmpty(rutaRelativa))
            {

                return null;
            }

            // Si la ruta relativa comienza con '~/', la sustituimos por la ruta absoluta del directorio ra�z del proyecto
            if (rutaRelativa.StartsWith("~/"))
            {
                var rootDir = AppDomain.CurrentDomain.BaseDirectory;
                rutaRelativa = rutaRelativa.Replace("~/", rootDir);
            }
            return rutaRelativa;
        }
    }
}
