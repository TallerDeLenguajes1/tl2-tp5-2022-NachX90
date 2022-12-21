using CadeteriaMVC.Controllers;
using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace CadeteriaMVC.Repositories
{
    public class CadetesRepository : IDBRepository<Empleado>
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IDBConnectionRepository _conexionBDRepository;

        public CadetesRepository(ILogger<CadetesController> logger, IDBConnectionRepository conexionBDRepository)
        {
            _logger = logger;
            _conexionBDRepository = conexionBDRepository;
        }

        public void Alta(Empleado Objeto, [Optional] int IdUsuario)
        {
            try
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
                var SentenciaSQL2 = $"insert into empleado (id, usuario, contrasena, idrol) values ({Id}, '{Objeto.Usuario}', '{Objeto.Contrasena}', 3);";
                using (var Conexion = _conexionBDRepository.ConexionSQLite())
                {
                    var Comando = new SQLiteCommand(SentenciaSQL2, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
                _logger.LogInformation($"El usuario {IdUsuario} cargó el cadete {Id}.");
            }
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en alta de cadete: {SQLiteEx.ToString()}");
                throw new Exception("Error en alta de cadete.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }

        public void BajaLogica(int Id, [Optional] int IdUsuario)
        {
            try
            {
                var SentenciaSQL = $"update persona set visible = 0 where id = {Id};";
                using (var Conexion = _conexionBDRepository.ConexionSQLite())
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
                _logger.LogInformation($"El usuario {IdUsuario} eliminó el cadete {Id}.");
            }
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en baja de cadete: {SQLiteEx.ToString()}");
                throw new Exception("Error en baja de cadete.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }

        public void Modificacion(Empleado Objeto, [Optional] int IdUsuario)
        {
            try
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
                _logger.LogInformation($"El usuario {IdUsuario} modificó el cadete {Objeto.Id}.");
            }
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en modificacion de cadete: {SQLiteEx.ToString()}");
                throw new Exception("Error en modificacion de cadete.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }

        public int ObtenerID(Empleado Objeto)
        {
            try
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
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en obtener id de cadete: {SQLiteEx.ToString()}");
                throw new Exception("Error en obtener id de cadete.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }

        public Empleado ObtenerPorID(int Id, [Optional] int IdUsuario)
        {
            try
            {
                Empleado Objeto = new();
                var SentenciaSQL = $"select nombre, domicilio, telefono, usuario, contrasena from empleado inner join persona using (id) where id = {Id} and visible = 1;";
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
                        }
                    }
                    Conexion.Close();
                }
                _logger.LogInformation($"El usuario {IdUsuario} solicitó el cadete {Id}.");
                return Objeto;
            }
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en obtener cadete: {SQLiteEx.ToString()}");
                throw new Exception("Error en obtener cadete.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }

        public List<Empleado> ObtenerTodos([Optional] int IdUsuario)
        {
            try
            {
                List<Empleado> ListaDeObjetos = new();
                var SentenciaSQL = "select id from empleado inner join persona using (id) where idrol = 3 and visible = 1;";
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
                _logger.LogInformation($"El usuario {IdUsuario} solicitó todos los cadetes.");
                return ListaDeObjetos;
            }
            catch (SQLiteException SQLiteEx)
            {
                _logger.LogDebug($"Error en obtener todos los cadetes: {SQLiteEx.ToString()}");
                throw new Exception("Error en obtener todos los cadetes.", SQLiteEx);
            }
            catch (Exception Ex)
            {
                _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
                throw new Exception("Error en la DB.", Ex);
            }
        }
    }
}
