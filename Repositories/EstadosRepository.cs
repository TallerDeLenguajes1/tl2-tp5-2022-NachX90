using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class EstadosRepository : IEstadosRepository
    {
        private readonly IConexionBDRepository _conexionBDRepository;

        public EstadosRepository(IConexionBDRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public List<Estado> ObtenerTodos()
        {
            List<Estado> ListaDeEstados = new();
            var SentenciaSQL = "select * from estado;";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var id = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var estado = string.Format("{0}", Reader[1]);
                        var descripcion = string.Format("{0}", Reader[2]);
                        Estado Estado = new(id, estado, descripcion);
                        ListaDeEstados.Add(Estado);
                    }
                }
                Conexion.Close();
            }
            return ListaDeEstados;
        }
    }
}
