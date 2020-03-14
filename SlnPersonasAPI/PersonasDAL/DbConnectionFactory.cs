using System.Data.SqlClient;

namespace PersonasDAL
{
    public class DbConnectionFactory
    {
        public static SqlConnection GetDbConnection(EDbConnectionTypes dbType, string connectionString)
        {
            SqlConnection connection = null;

            switch (dbType)
            {
                case EDbConnectionTypes.Sql:
                    connection = new SqlConnection(connectionString);
                    break;
                default:
                    connection = null;
                    break;
            }
            return connection;
        }
    }

    public enum EDbConnectionTypes
    {
        Sql,
        Mongo,
        Xml
    }
}
