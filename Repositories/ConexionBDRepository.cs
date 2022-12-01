using System.Data.SQLite;

namespace CadeteriaMVC.Repositories
{
    public class ConexionBDRepository: IConexionBDRepository
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

        // Aquí iría una conexión a otro tipo de DB
        //public OtroDBTypeConnection ConexionOtroDBType()
        //{
        //    return new OtroDBTypeConnection(_configuration["ConnectionStrings:ConexionOtroDBType"]);
        //}
    }
}
