using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace CadeteriaMVC.Repositories
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly IDBConnectionRepository _conexionBDRepository;

        public PedidosRepository(IDBConnectionRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public void Alta(Pedido Objeto, [Optional] int IdUsuario)
        {
            var SentenciaSQL = $"insert into pedido (pedido, idcliente, idcadete, idestado) values ('{Objeto.Descripcion}', {Objeto.IdCliente}, {Objeto.IdCadete}, {Objeto.IdEstado});";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void BajaLogica(int Id, [Optional] int IdUsuario)
        {
            var SentenciaSQL = $"update pedido set visible = 0 where id = {Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Modificacion(Pedido Objeto, [Optional] int IdUsuario)
        {
            var SentenciaSQL = $"update pedido set pedido = '{Objeto.Descripcion}', idcliente = {Objeto.IdCliente}, idcadete = {Objeto.IdCadete}, idestado = {Objeto.IdEstado} where id = {Objeto.Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public int ObtenerID(Pedido Objeto)
        {
            return Objeto.Id;       // Esto no me sirve pero debo implementarlo porque está en la interfaz
        }

        public Pedido ObtenerPorID(int Id, [Optional] int IdUsuario)
        {
            Pedido Objeto = new();
            var SentenciaSQL = $"select P.pedido, P.idcliente, CL.nombre, P.idcadete, CA.nombre, P.idestado, E.estado from pedido P inner join (select id, nombre from cliente inner join persona using (id)) CL on P.idcliente = CL.id left join (select id, nombre from empleado inner join persona using (id)) CA on P.idcadete = CA.id inner join estado E on P.idestado = E.id where P.id = {Id} and P.visible = 1;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Objeto.Id = Id;
                        Objeto.Descripcion = Reader[0].ToString();
                        Objeto.IdCliente = Convert.ToInt32(Reader[1]);
                        Objeto.Cliente = Reader[2].ToString();
                        Objeto.IdCadete = Convert.ToInt32(Reader[3]);
                        Objeto.Cadete = Reader[4].ToString();
                        Objeto.IdEstado = Convert.ToInt32(Reader[5]);
                        Objeto.Estado = Reader[6].ToString();
                    }
                }
                Conexion.Close();
            }
            return Objeto;
        }

        public List<Pedido> ObtenerTodos([Optional] int IdUsuario)
        {
            List<Pedido> ListaDeObjetos = new();
            var SentenciaSQL = "select id from pedido where visible = 1;";
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

        public List<Pedido> ObtenerPorCadete(int Id, [Optional] int IdUsuario)
        {
            List<Pedido> ListaDeObjetos = new();
            var SentenciaSQL = $"select id from pedido where idcadete = {Id} and visible = 1;";
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

        public List<Pedido> ObtenerPorCliente(int Id, [Optional] int IdUsuario)
        {
            List<Pedido> ListaDeObjetos = new();
            var SentenciaSQL = $"select id from pedido where idcliente = {Id} and visible = 1;";
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
}
