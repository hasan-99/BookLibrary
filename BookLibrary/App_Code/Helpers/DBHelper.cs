

using System.Configuration;
using System.Data.SqlClient;

namespace BookLibrary.App_Code.Helpers;

public static SqlConnection GetConnection()
{
    return new SqlConnection(
        ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString
        );
}
