using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Snt.Romashka.Db.Core.Abstracts;
using Snt.Romashka.Repositories.Concretes;

namespace Snt.Romashka.Database.Core
{
    public static class CommonDbHelper
    {
        public static T[] Query<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.Query<T>(connection, query, parameters).ToArray();
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.ExecuteScalar<T>(connection, query, parameters);
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string query, object parameters = null, IDbTransaction tran = null)
        {
            return Dapper.SqlMapper.Execute(connection, query, parameters, tran);
        }

        public static TReturn[] Query<T1, T2, TReturn>(this IDbConnection connection, string query, Func<T1, T2, TReturn> p,
            object parameters = null)
        {
            return Dapper.SqlMapper.Query(connection, query, p, parameters).ToArray();
        }

        public static Task<T[]> QueryAsync<T>(this IDbConnection connection, string query, object parameters = null, IDbTransaction tran = null)
        {
            return Dapper.SqlMapper.QueryAsync<T>(connection, query, parameters, tran).ToArrayAsync();
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.ExecuteScalarAsync<T>(connection, query, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, query, parameters);
        }

        public static Task<TReturn[]> QueryAsync<T1, T2, TReturn>(this IDbConnection connection, string query, Func<T1, T2, TReturn> p,
            object parameters = null)
        {
            return Dapper.SqlMapper.QueryAsync(connection, query, p, parameters).ToArrayAsync();
        }

        public static T QueryFirst<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QueryFirst<T>(connection, query, parameters);
        }

        public static T QueryFirstOrDefault<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QueryFirstOrDefault<T>(connection, query, parameters);
        }

        public static T QuerySingle<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QuerySingle<T>(connection, query, parameters);
        }

        public static T QuerySingleOrDefault<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QuerySingleOrDefault<T>(connection, query, parameters);
        }

        public static Task<T> QueryFirstAsync<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QueryFirstAsync<T>(connection, query, parameters);
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QueryFirstOrDefaultAsync<T>(connection, query, parameters);
        }

        public static Task<T> QuerySingleAsync<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QuerySingleAsync<T>(connection, query, parameters);
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection connection, string query, object parameters = null)
        {
            return Dapper.SqlMapper.QuerySingleOrDefaultAsync<T>(connection, query, parameters);
        }
    }
}