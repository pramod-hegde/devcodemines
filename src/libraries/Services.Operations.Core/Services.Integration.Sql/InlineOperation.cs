using System.Data;
using System.Data.SqlClient;

namespace Services.Integration.Sql
{
    [SqlOperationMetadata(SqlCommandTypes.Text)]
    sealed class InlineOperation : ISqlOperation
    {
        readonly IConnectionManager _connectionManager = default;
        readonly ISqlConfiguration _executionMetadata = default;

        public InlineOperation(IConnectionManager connectionManager, ISqlConfiguration executionMetadata)
        {
            _connectionManager = connectionManager;
            _executionMetadata = executionMetadata;
        }

        object ISqlOperation.Execute<TIn>(params TIn[] parameters)
        {
            using SqlCommand command = new SqlCommand()
            {
                CommandTimeout = 0,
                CommandType = CommandType.Text,
                Connection = _connectionManager.Connection
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.CommandText = string.Format(_executionMetadata.CommandText, parameters);
            }

            return _executionMetadata.ExecutionMode switch
            {
                SqlExecutionModes.Scalar => command.ExecuteScalar(),
                SqlExecutionModes.TSql => command.ExecuteNonQuery(),
                SqlExecutionModes.TSqlSave => command.ExecuteNonQuery(),
                SqlExecutionModes.DataReader => command.ExecuteReader(),
                _ => default,
            };
        }
    }
}
