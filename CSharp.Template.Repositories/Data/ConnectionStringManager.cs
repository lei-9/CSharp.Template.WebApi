using System;

namespace CSharp.Template.Repositories.Data
{
    public class ConnectionStringManager
    {
        private static  string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = "";

                return _connectionString;
            }
        }
    }
}