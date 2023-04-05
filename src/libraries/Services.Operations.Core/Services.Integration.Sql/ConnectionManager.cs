using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Services.Integration.Sql
{
    sealed class ConnectionManager : IConnectionManager
    {
        SqlConnection _connection = default;
        ISqlConfiguration _metadata = default;
       
        internal ConnectionManager(ISqlConfiguration metadata)
        {
            _metadata = metadata;
        }

        SqlConnection IConnectionManager.Connection => _connection;

        async Task IConnectionManager.Close()
        {
            if (_connection != default && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
                GC.Collect();
            }
        }

        async Task IConnectionManager.Establish()
        {
            _connection = new SqlConnection();

            var sqlAuthConfig = (string[])_metadata.AuthenticationConfig.AuthenticationCallback();

            if (sqlAuthConfig[0]== "DirectConnection" || sqlAuthConfig[0] == "SqlVaultSecretConnection")
            {
                _connection.ConnectionString = sqlAuthConfig[1];               
            }
            else
            {
                _connection.ConnectionString = sqlAuthConfig[1];
                _connection.AccessToken = sqlAuthConfig[2];
            }

            await _connection.OpenAsync();
        }
    }
}
