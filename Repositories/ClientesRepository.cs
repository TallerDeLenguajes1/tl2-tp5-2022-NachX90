using CadeteriaMVC.Controllers;
using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories;

public class ClientesRepository : IDBRepository <Cliente>
{
    private readonly ILogger<CadetesController> _logger;
    private readonly IDBConnectionRepository _conexionBDRepository;

    public ClientesRepository(ILogger<CadetesController> logger, IDBConnectionRepository conexionBDRepository)
    {
        _logger = logger;
        _conexionBDRepository = conexionBDRepository;
    }

    public void Alta(Cliente Objeto)
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
            var SentenciaSQL2 = $"insert into cliente (id, referencia) values ({Id}, '{Objeto.Referencia}');";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL2, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            _logger.LogInformation($"Se cargó el cliente {Id}.");
        }
        catch (SQLiteException SQLiteEx)
        {
            _logger.LogDebug($"Error en alta de cliente: {SQLiteEx.ToString()}");
            throw new Exception("Error en alta de cliente.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }

    public void BajaLogica(int Id)
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
            _logger.LogInformation($"Se eliminó el cliente {Id}.");
        }
        catch (SQLiteException SQLiteEx)
        {
            _logger.LogDebug($"Error en baja de cliente: {SQLiteEx.ToString()}");
            throw new Exception("Error en baja de cliente.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }

    public void Modificacion(Cliente Objeto)
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
            var SentenciaSQL2 = $"update cliente set referencia = '{Objeto.Referencia}' where id = {Objeto.Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL2, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            _logger.LogInformation($"Se modificó el cliente {Objeto.Id}.");
        }
        catch (SQLiteException SQLiteEx)
        {
            _logger.LogDebug($"Error en modificación de cliente: {SQLiteEx.ToString()}");
            throw new Exception("Error en modificación de cliente.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }

    public int ObtenerID(Cliente Objeto)
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
            _logger.LogDebug($"Error en obtener id de cliente: {SQLiteEx.ToString()}");
            throw new Exception("Error en obtener id de cliente.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }

    public Cliente ObtenerPorID(int Id)
    {
        try
        {
            Cliente Objeto = new();
            var SentenciaSQL = $"select nombre, domicilio, telefono, referencia from cliente inner join persona using (id) where id = {Id} and visible = 1;";
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
                        Objeto.Referencia = Reader[3].ToString();
                    }
                }
                Conexion.Close();
            }
            _logger.LogInformation($"Se solicitó el cliente {Id}.");
            return Objeto;
        }
        catch (SQLiteException SQLiteEx)
        {
            _logger.LogDebug($"Error en obtener cliente: {SQLiteEx.ToString()}");
            throw new Exception("Error en obtener cliente.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }

    public List<Cliente> ObtenerTodos()
    {
        try
        {
            List<Cliente> ListaDeObjetos = new();
            var SentenciaSQL = "select id from cliente inner join persona using (id) where visible = 1;";
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
            _logger.LogInformation($"Se solicitaron todos los clientes.");
            return ListaDeObjetos;
        }
        catch (SQLiteException SQLiteEx)
        {
            _logger.LogDebug($"Error en obtener todos los clientes: {SQLiteEx.ToString()}");
            throw new Exception("Error en obtener todos los clientes.", SQLiteEx);
        }
        catch (Exception Ex)
        {
            _logger.LogDebug($"Error en la DB: {Ex.ToString()}");
            throw new Exception("Error en la DB.", Ex);
        }
    }
}
