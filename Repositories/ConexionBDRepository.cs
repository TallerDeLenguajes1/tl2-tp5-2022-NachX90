using System.Data.SQLite;
using CadeteriaMVC.Interfaces;

namespace CadeteriaMVC.Repositories
{
    public class ConexionBDRepository: IDBConnectionRepository
    {
        private readonly IConfiguration _configuration;

        public ConexionBDRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SQLiteConnection ConexionSQLite()
        {
            return new SQLiteConnection(_configuration["ConnectionStrings:ConexionSQLite"]);
        }

        // Aquí irían las conexiones a otras DB
        //public DBTypeConnection ConexionOtraDB()
        //{
        //    return new DBTypeConnection(_configuration["ConnectionStrings:ConexionOtraDB"]);
        //}
    }
}
