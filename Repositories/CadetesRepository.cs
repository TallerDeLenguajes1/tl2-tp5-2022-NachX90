using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class CadetesRepository : ICadetesRepository
    {
        private readonly IConexionBDRepository _conexionBDRepository;

        public CadetesRepository(IConexionBDRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public void Crear(Cadete Cadete)
        {
            var SentenciaSQL = $"insert into cadete (cadete, direccion, telefono) values ('{Cadete.Nombre}', '{Cadete.Direccion}', {Cadete.Telefono});";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public void Editar(Cadete Cadete)
        {
            var SentenciaSQL = $"update cadete set cadete='{Cadete.Nombre}', direccion='{Cadete.Direccion}', telefono='{Cadete.Telefono}' where id_cadete={Cadete.Id};";
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
            var SentenciaSQL = $"delete from cadete where id_cadete={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
        }

        public Cadete Obtener(int Id)
        {
            Cadete Cadete = new();
            var SentenciaSQL = $"select cadete, direccion, telefono from cadete where id_cadete={Id};";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Cadete.Id = Convert.ToUInt32(Id);
                        Cadete.Nombre = string.Format("{0}", Reader[0]);
                        Cadete.Direccion = string.Format("{0}", Reader[1]);
                        Cadete.Telefono = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                    }
                }
                Conexion.Close();
            }
            return Cadete;
        }

        public List<Cadete> ObtenerTodos()
        {
            List<Cadete> ListaDeCadetes = new();
            var SentenciaSQL = "select id_cadete from cadete;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var Cadete = Obtener(Convert.ToInt32(string.Format("{0}", Reader[0])));
                        ListaDeCadetes.Add(Cadete);
                    }
                }
                Conexion.Close();
            }
            return ListaDeCadetes;
        }
    }
}
