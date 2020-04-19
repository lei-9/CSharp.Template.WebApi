using System;
using MySql.Data.MySqlClient;

namespace CSharp.Template.Repositories.Data
{
    public class ConnectionManager
    {
        private static MySqlConnection _connection;

        public static MySqlConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new MySqlConnection(ConnectionStringManager.ConnectionString);

                return _connection;
            }
        }
    }
}