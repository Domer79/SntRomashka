using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Snt.Romashka.Db.Core.Abstracts;

namespace Snt.Romashka.Database.Core
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IGlobalSettings _globalSettings;

        public SqlConnectionFactory(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        private string ConnectionString => GetConnectionString();

        private string GetConnectionString()
        {
            return _globalSettings.DefaultConnectionString;
        }

        public Func<IDbConnection> CreateConnection => GetConnection;
        
        private IDbConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
