using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly IConexionBDRepository _conexionBDRepository;

        public ClientesRepository(IConexionBDRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public void Crear(Cliente Cliente)
        {
            var SentenciaSQL = $"insert into cliente (cliente, direccion, referencia, telefono) values ('{Cliente.Nombre}', '{Cliente.Direccion}', '{Cliente.ReferenciaDeDireccion}', '{Cliente.Telefono}');";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Editar(Cliente Cliente)
        {
            var SentenciaSQL = $"update cliente set cliente='{Cliente.Nombre}', direccion='{Cliente.Direccion}', referencia='{Cliente.ReferenciaDeDireccion}', telefono={Cliente.Telefono} where id_cliente={Cliente.Id};";
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
            var SentenciaSQL = $"delete from cliente where id_cliente={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public Cliente Obtener(int Id)
        {
            Cliente Cliente = new();
            var SentenciaSQL = $"select cliente, direccion, referencia, telefono from cliente where id_cliente={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Cliente.Id = Convert.ToUInt32(Id);
                        Cliente.Nombre = string.Format("{0}", Reader[0]);
                        Cliente.Direccion = string.Format("{0}", Reader[1]);
                        Cliente.ReferenciaDeDireccion = string.Format("{0}", Reader[2]);
                        Cliente.Telefono = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                    }
                }
                Conexion.Close();
            }
            return Cliente;
        }

        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> ListaDeClientes = new();
            var SentenciaSQL = "select id_cliente from cliente;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Cliente = Obtener(Convert.ToInt32(string.Format("{0}", Reader[0])));
                        ListaDeClientes.Add(Cliente);
                    }
                }
                Conexion.Close();
            }
            return ListaDeClientes;
        }
    }
}
