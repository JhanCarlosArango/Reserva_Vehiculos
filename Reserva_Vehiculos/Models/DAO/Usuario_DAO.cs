using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Reserva_Vehiculos.Models;
using System.Configuration;
using Npgsql;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Usuario_DAO
    {
        private readonly string _connectionString;
        public Usuario_DAO()
        {
            _connectionString = "Host=reservas.chea08gwkn1d.us-east-1.rds.amazonaws.com;Port=5432;Database=reserva;Username=jkgamer;Password=01760091;";
        }

        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> l_usuarios = new List<Usuarios>();
            var cn = new Conexion();
            l_usuarios = new List<Usuarios>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM USUARIO;";  
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Usuarios user = new Usuarios();
                                user.usuario = dr["usuario"].ToString();
                                user.contrasenia = dr["contrasenia"].ToString();
                                Console.WriteLine(user.contrasenia);
                                Console.WriteLine(user.usuario);
                                l_usuarios.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar usuarios: {ex.Message}");
            }

            return l_usuarios;
        }
        public Boolean validar_User(String user, String pass)
        {
            Boolean auth = false;
            List<Usuarios> l_us = new List<Usuarios>();
            l_us = ListarUsuarios();
            foreach (Usuarios item in l_us)
            {
                if (item.Get_Usuario().Equals(user))
                {
                    if (item.Get_Contrasenia().Equals(pass))
                    {
                        auth = true;
                    }
                }
            }
            return auth;
        }
    }

}
/*
   public boolean validarUser(String user, String pass) {

        boolean vali = false;
        List<Usuario> listauser = new ArrayList<Usuario>();
        listauser = controlPersis.listareUsuarios_JPA();
        for (Usuario usuario : listauser) {
            if (usuario.getNom_Usuario().equals(user)) {
                if (usuario.getPassword().equals(pass)) {
                    vali = true;
                }
            }
        }
        return vali;
    }

    public List<ContactoModel> Listar() {
    var oLista = new List<ContactoModel>();
    var cn = new Conexion();
    using (var conexion = new SqlConnection(cn.getCadenaSQL())) {
        conexion.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM usuarios", conexion);
        using (var dr = cmd.ExecuteReader()) {
            while (dr.Read()) {
                oLista.Add(new ContactoModel() {
                    IdContacto = Convert.ToInt32(dr["IdContacto"]),
                    Nombre = dr["Nombre"].ToString(),
                    Telefono = dr["Telefono"].ToString(),
                    Correo = dr["Correo"].ToString()
                });
            }
        }
        return oLista;
    }
}


        public List<Usuarios> listar_Users(){
            user = new Usuarios();
            l_usuarios = new List<Usuarios>();
            Conexion cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL())){
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM usuarios", conexion);
                using (var dr = cmd.ExecuteReader()){
                    while (dr.Read()){
                        user = new Usuarios();
                        user.Set_Usuario(dr["usuario"].ToString());
                        user.Set_Contrasenia(dr["contrasenia"].ToString());
                        l_usuarios.Add(user);
                    }
                }

                return l_usuarios;
            }
        }
*/