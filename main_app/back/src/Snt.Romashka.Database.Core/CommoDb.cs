using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Snt.Romashka.Db.Core.Abstracts;

namespace Snt.Romashka.Database.Core
{
    public class CommonDb : ICommonDb
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly Stack<CommonDbTransactionWrapper> _transactions = new Stack<CommonDbTransactionWrapper>();

        public CommonDb(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public T[] Query<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.Query<T>(query, parameters);
        }

        public T ExecuteScalar<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.ExecuteScalar<T>(query, parameters);
        }

        public int ExecuteNonQuery(string query, object parameters = null)
        {
            var connection = GetConnection();
            try
            {
                return connection.ExecuteNonQuery(query, parameters, GetTransaction());
            }
            finally
            {
                ConnectionDispose(connection);
            }
        }

        public void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            collectionInfo.Count = ExecuteScalar<int>(query);
            collectionInfo.PageCount = collectionInfo.Count / pageSize + (collectionInfo.Count % pageSize > 0 ? 1 : 0);
        }

        public TReturn[] Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p,
            object parameters = null)
        {
            using var connection = GetConnection();
            return connection.Query(query, p, parameters);
        }

        public Task<T[]> QueryAsync<T>(string query, object parameters = null)
        {
            var connection = GetConnection();
            try
            {
                return connection.QueryAsync<T>(query, parameters, GetTransaction());
            }
            finally
            {
                ConnectionDispose(connection);
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.ExecuteScalarAsync<T>(query, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task GetPageCountAsync(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            collectionInfo.Count = await ExecuteScalarAsync<int>(query);
            collectionInfo.PageCount = collectionInfo.Count / pageSize + (collectionInfo.Count % pageSize > 0 ? 1 : 0);
        }

        public Task<TReturn[]> QueryAsync<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p,
            object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QueryAsync(query, p, parameters);
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QueryFirst<T>(query, parameters);
        }

        public T QueryFirstOrDefault<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<T>(query, parameters);
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QuerySingle<T>(query, parameters);
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QuerySingleOrDefault<T>(query, parameters);
        }

        public Task<T> QueryFirstAsync<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QueryFirstAsync<T>(query, parameters);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            using var connection = GetConnection();
            return connection.QuerySingleAsync<T>(query, parameters);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            var connection = GetConnection();
            try
            {
                return connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
            finally
            {
                ConnectionDispose(connection);
            }
        }

        public IDbConnection GetConnection()
        {
            return _transactions.Any() ? _transactions.Peek().Connection : _connectionFactory.CreateConnection();
        }

        public CommonDbTransactionWrapper BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var connection = GetConnection();
            var transaction = new CommonDbTransactionWrapper(connection, isolationLevel);
            transaction.Disposing = TransactionDispose;
            _transactions.Push(transaction);
            return transaction;
        }

        private void TransactionDispose()
        {
            _transactions.Pop();
        }
        
        private void ConnectionDispose(IDbConnection connection)
        {
            if (!_transactions.Any())
            {
                connection.Dispose();
            }
        }

        private IDbTransaction GetTransaction()
        {
            return _transactions.Any()
                ? _transactions.Peek().GetTarget()
                : null;
        }
    }
}