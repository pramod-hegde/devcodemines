using System;

namespace Services.Integration.Sql
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SqlOperationMetadataAttribute : Attribute
    {
        public SqlOperationMetadataAttribute(SqlCommandTypes commandType)
        {
            CommandType = commandType;
        }

        public SqlCommandTypes CommandType { get; }
    }
}
