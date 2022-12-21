using System.Data.SQLite;

namespace CadeteriaMVC.Interfaces;

public interface IDBConnectionRepository
{
    SQLiteConnection ConexionSQLite();
}
