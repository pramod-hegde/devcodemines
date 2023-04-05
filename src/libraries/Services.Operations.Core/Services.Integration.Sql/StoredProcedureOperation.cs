using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services.Integration.Sql
{
    [SqlOperationMetadata(SqlCommandTypes.StoredProcedure)]
    sealed class StoredProcedureOperation : ISqlOperation
    {
        readonly IConnectionManager _connectionManager = default;
        readonly ISqlConfiguration _executionMetadata = default;

        public StoredProcedureOperation(IConnectionManager connectionManager, ISqlConfiguration executionMetadata)
        {
            _connectionManager = connectionManager;
            _executionMetadata = executionMetadata;
        }

        object ISqlOperation.Execute<TIn>(params TIn[] parameters)
        {
            using SqlCommand command = new SqlCommand(_executionMetadata.CommandText, _connectionManager.Connection)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter is default(SqlParameter))
                    {
                        continue;
                    }
                    if (parameter is KeyValuePair<string, object>)
                    {
                        var p = (KeyValuePair<string, object>)Convert.ChangeType(parameter, typeof(KeyValuePair<string, object>));
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                    }
                    else if (parameter is KeyValuePair<string, SqlDbType>)
                    {
                        var p = (KeyValuePair<string, SqlDbType>)Convert.ChangeType(parameter, typeof(KeyValuePair<string, SqlDbType>));
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                    }
                    else
                    {
                        command.Parameters.Add(parameter);
                    }
                }
            }

            return _executionMetadata.ExecutionMode switch
            {
                SqlExecutionModes.Scalar => command.ExecuteScalar(),
                SqlExecutionModes.TSql => ReadProcData(command),
                SqlExecutionModes.TSqlSave => command.ExecuteNonQuery(),
                SqlExecutionModes.DataReader => command.ExecuteReader(),
                _ => default,
            };
        }

        private object ReadProcData(SqlCommand command)
        {
            using SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            return set;
        }
    }
}
