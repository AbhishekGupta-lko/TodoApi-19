using System.Data.SqlClient;

namespace TodoApi.Models
{
    public static class CommonVariables
    {
        public static string ConnectionString { get; set; }
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static int SqlCommandTimeout
        {
            get
            {
                return 90;
            }
        }
    }
}
