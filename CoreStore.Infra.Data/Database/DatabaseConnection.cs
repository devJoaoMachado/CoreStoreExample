using System;
using System.Data.SqlClient;

namespace CoreStore.Infra.Data.Data
{
    public class DatabaseConnection : IDisposable
    {
        private SqlConnection _connection;

        public DatabaseConnection(ConnectionSettings connectionSettings)
        {
            _connection = new SqlConnection(connectionSettings.ConnectionString);
            _connection.Open();
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            if(_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
