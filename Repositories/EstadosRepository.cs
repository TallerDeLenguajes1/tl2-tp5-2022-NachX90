using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories;

public class EstadosRepository : IDBRepository <Estado>
{
    private readonly IDBConnectionRepository _conexionBDRepository;

    public EstadosRepository(IDBConnectionRepository conexionBDRepository)
    {
        _conexionBDRepository = conexionBDRepository;
    }

    public void Alta(Estado Objeto)
    {
        var SentenciaSQL = $"insert into estado (estado, descripcion) values ('{Objeto.NombreEstado}', '{Objeto.Descripcion}');";
        using (var Conexion = _conexionBDRepository.ConexionSQLite())
        {
            var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
            Conexion.Open();
            Comando.ExecuteNonQuery();
            Conexion.Close();
        }
    }

    public void BajaLogica(int Id)
    {
        var SentenciaSQL = $"update estado set visible = 0 where id = {Id};";
        using (var Conexion = _conexionBDRepository.ConexionSQLite())
        {
            var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
            Conexion.Open();
            Comando.ExecuteNonQuery();
            Conexion.Close();
        }
    }

    public void Modificacion(Estado Objeto)
    {
        var SentenciaSQL = $"update estado set estado = '{Objeto.NombreEstado}', descripcion = '{Objeto.Descripcion}' where id = {Objeto.Id};";
        using (var Conexion = _conexionBDRepository.ConexionSQLite())
        {
            var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
            Conexion.Open();
            Comando.ExecuteNonQuery();
            Conexion.Close();
        }
    }

    public int ObtenerID(Estado Objeto)
    {
        int Id = 0;
        var SentenciaSQL = $"select id from estado where estado = {Objeto.NombreEstado};";
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

    public Estado ObtenerPorID(int Id)
    {
        Estado Objeto = new();
        var SentenciaSQL = $"select estado, descripcion from estado where id = {Id} and visible = 1;";
        using (var Conexion = _conexionBDRepository.ConexionSQLite())
        {
            var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
            Conexion.Open();
            using (SQLiteDataReader Reader = Comando.ExecuteReader())
            {
                while (Reader.Read())
                {
                    Objeto.Id = Id;
                    Objeto.NombreEstado = Reader[0].ToString();
                    Objeto.Descripcion = Reader[1].ToString();
                }
            }
            Conexion.Close();
        }
        return Objeto;
    }

    public List<Estado> ObtenerTodos()
    {
        List<Estado> ListaDeObjetos = new();
        var SentenciaSQL = "select id from estado where visible = 1;";
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
}
