using System;
using System.Data;

namespace Snt.Romashka.Db.Core.Abstracts
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}