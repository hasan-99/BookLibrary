using System.Configuration;
using System.Data.SqlClient;

namespace BookLibrary.Helpers {
    public static class DBHelper {
        public static SqlConnection GetConnection() {
            var cs = ConfigurationManager.ConnectionStrings["BooksDB"].ConnectionString;
            return new SqlConnection(cs);
        }
    }
}