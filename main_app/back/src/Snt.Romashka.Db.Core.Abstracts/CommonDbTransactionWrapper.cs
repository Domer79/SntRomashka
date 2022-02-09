using System;
using System.Data;

namespace Snt.Romashka.Db.Core.Abstracts
{
    public sealed class CommonDbTransactionWrapper: IDisposable
    {
        private readonly IDbTransaction _transaction;
        private bool _closed;

        public CommonDbTransactionWrapper(IDbConnection connection, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Connection = connection;
            _transaction = connection.BeginTransaction(isolationLevel);
        }

        public IDbConnection Connection { get; }

        public IDbTransaction GetTarget () => _transaction;

        public Action Disposing { get; set; }

        public void Commit()
        {
            _transaction.Commit();
            _closed = true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _closed = true;
        }

        public void Dispose()
        {
            if (!_closed)
            {
                _transaction.Rollback();
            }

            _transaction.Dispose();
            Disposing?.Invoke();
        }
    }
    
    public class CommonDbConnection: IDbConnection
    {
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; }
        public string Database { get; }
        public ConnectionState State { get; }
    }
}