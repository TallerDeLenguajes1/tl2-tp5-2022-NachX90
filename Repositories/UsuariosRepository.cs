using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace CadeteriaMVC.Repositories
{
    public class UsuariosRepository : IEmpleadosRepository
    {
        private readonly IDBConnectionRepository _conexionBDRepository;

        public UsuariosRepository(IDBConnectionRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public void Alta(Empleado Objeto)
        {
            var SentenciaSQL1 = $"insert into persona (nombre, domicilio, telefono) values ('{Objeto.Nombre}', '{Objeto.Domicilio}', {Objeto.Telefono});";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL1, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            var Id = ObtenerID(Objeto);
            var SentenciaSQL2 = $"insert into empleado (id, usuario, contrasena, idrol) values ({Id}, '{Objeto.Usuario}', '{Objeto.Contrasena}', {Objeto.IDRol});";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL2, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void BajaLogica(int Id)
        {
            var SentenciaSQL = $"update persona set visible = 0 where id = {Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Modificacion(Empleado Objeto)
        {
            var SentenciaSQL1 = $"update persona set nombre = '{Objeto.Nombre}', domicilio = '{Objeto.Domicilio}', telefono = {Objeto.Telefono} where id = {Objeto.Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL1, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            var SentenciaSQL2 = $"update empleado set usuario = '{Objeto.Usuario}', contrasena = '{Objeto.Contrasena}' where id = {Objeto.Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL2, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public int ObtenerID(Empleado Objeto)
        {
            int Id = 0;
            var SentenciaSQL = $"select id from persona where telefono = {Objeto.Telefono};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Id = Convert.ToInt32(Reader[0]);
                    }
                }
                Conexion.Close();
            }
            return Id;
        }

        public Empleado ObtenerPorID(int Id)
        {
            Empleado Objeto = new();
            var SentenciaSQL = $"select nombre, domicilio, telefono, usuario, contrasena, idrol, rol from empleado E inner join persona P on E.id=P.id inner join rol R on E.idrol=R.id where E.id = {Id} and visible = 1;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Objeto.Id = Id;
                        Objeto.Nombre = Reader[0].ToString();
                        Objeto.Domicilio = Reader[1].ToString();
                        Objeto.Telefono = Convert.ToInt64(Reader[2]);
                        Objeto.Usuario = Reader[3].ToString();
                        Objeto.Contrasena = Reader[4].ToString();
                        Objeto.IDRol = Convert.ToInt32(Reader[5]);
                        Objeto.Rol = Reader[6].ToString();
                    }
                }
                Conexion.Close();
            }
            return Objeto;
        }

        public List<Empleado> ObtenerTodos()
        {
            List<Empleado> ListaDeObjetos = new();
            var SentenciaSQL = "select id from empleado inner join persona using (id) where visible = 1;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Objeto = ObtenerPorID(Convert.ToInt32(Reader[0]));
                        ListaDeObjetos.Add(Objeto);
                    }
                }
                Conexion.Close();
            }
            return ListaDeObjetos;
        }

        public Empleado Verificar(Empleado Objeto)
        {
            int Id = 0;
            var SentenciaSQL1 = $"select id from empleado where usuario = '{Objeto.Usuario}' and contrasena = '{Objeto.Contrasena}';";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL1, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Id = Convert.ToInt32(Reader[0]);
                    }
                }
                Conexion.Close();
            }
            Objeto = ObtenerPorID(Id);
            return Objeto;
        }
    }
}
