using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly IConexionBDRepository _conexionBDRepository;

        public PedidosRepository(IConexionBDRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public void Crear(Pedido Pedido)
        {
            var SentenciaSQL = $"insert into pedido (pedido, id_cliente, id_cadete, id_estado) values ('{Pedido.Obs}', {Pedido.IdCliente}, {Pedido.IdCadete}, {Pedido.IdEstado});";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Editar(Pedido Pedido)
        {
            var SentenciaSQL = $"update pedido set pedido='{Pedido.Obs}', id_cliente={Pedido.IdCliente}, id_cadete={Pedido.IdCadete}, id_estado={Pedido.IdEstado} where id_pedido={Pedido.Nro};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Eliminar(int Id)
        {
            var SentenciaSQL = $"delete from pedido where id_pedido={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public Pedido Obtener(int Id)
        {
            Pedido Pedido = new();
            var SentenciaSQL = $"select pedido, id_cliente, cliente, id_cadete, cadete, id_estado, estado from pedido left join cliente using(id_cliente) left join cadete using(id_cadete) inner join estado using (id_estado) where id_pedido={Id};";    // Los join son para mostrar los nombres en los listados
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Pedido.Nro = Convert.ToUInt32(Id);
                        Pedido.Obs = string.Format("{0}", Reader[0]);
                        Pedido.IdCliente = Convert.ToUInt32(string.Format("{0}", Reader[1]));
                        Pedido.Cliente = string.Format("{0}", Reader[2]);
                        Pedido.IdCadete = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        Pedido.Cadete = string.Format("{0}", Reader[4]);
                        Pedido.IdEstado = Convert.ToUInt32(string.Format("{0}", Reader[5]));
                        Pedido.Estado = string.Format("{0}", Reader[6]);
                    }
                }
                Conexion.Close();
            }
            return Pedido;
        }

        public List<Pedido> ObtenerTodos()
        {
            List<Pedido> ListaDePedidos = new();
            var SentenciaSQL = "select id_pedido from pedido;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Pedido = Obtener(Convert.ToInt32(string.Format("{0}", Reader[0])));
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
            return ListaDePedidos;
        }

        public List<Pedido> ObtenerPorCadete(int Id)
        {
            List<Pedido> ListaDePedidos = new();
            var SentenciaSQL = $"select id_pedido from pedido where id_cadete={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Pedido = Obtener(Convert.ToInt32(string.Format("{0}", Reader[0])));
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
            return ListaDePedidos;
        }

        public List<Pedido> ObtenerPorCliente(int Id)
        {
            List<Pedido> ListaDePedidos = new();
            var SentenciaSQL = $"select id_pedido from pedido where id_cliente={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Pedido = Obtener(Convert.ToInt32(string.Format("{0}", Reader[0])));
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
            return ListaDePedidos;
        }
    }
}
