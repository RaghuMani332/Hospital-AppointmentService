using Microsoft.Data.SqlClient;
using System.Data;

namespace Hospital_HospitalService.Context
{
    public class DapperContext(IConfiguration config)
    {
        public IDbConnection getConnection() => new SqlConnection(config.GetConnectionString("SqlConnection"));
    }
}
