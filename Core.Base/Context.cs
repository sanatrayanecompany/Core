using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.Base
{
    internal class Context : IContext, IDisposable
    {
        private IDbConnection _connection;
        public IDbConnection Data => _connection ?? (_connection = new SqlConnection(Host.Config.ConnectionString));

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
            }
        }
    }
}