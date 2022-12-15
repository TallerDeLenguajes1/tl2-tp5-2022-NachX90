using CadeteriaMVC.Enums;
using CadeteriaMVC.Models;
using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly IConexionBDRepository _conexionBDRepository;

        public UsuariosRepository(IConexionBDRepository conexionBDRepository)
        {
            _conexionBDRepository = conexionBDRepository;
        }

        public Usuario Verificar(Usuario Usuario)
        {
            var SentenciaSQL = $"select nombre, id_rol, id_cadete from usuario where usuario = '{Usuario.Nickname}' and contrasena = '{Usuario.Contrasena}';";
            using (var Conexion = _conexionBDRepository.ConexionSQLite())
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Usuario.Nombre = string.Format("{0}", Reader[0]);
                        Usuario.IdRol = Convert.ToUInt32(string.Format("{0}", Reader[1]));
                        if (Usuario.IdRol == (int)Roles.Cadete)
                        {
                            Usuario.IdCadete = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                        }
                    }
                }
                Conexion.Close();
            }
            return Usuario;
        }
    }
}
