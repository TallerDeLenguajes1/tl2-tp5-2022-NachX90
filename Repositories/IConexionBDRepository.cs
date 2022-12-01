using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public interface IConexionBDRepository
    {
        SQLiteConnection ConexionSQLite();
    }
}
