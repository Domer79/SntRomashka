using System.Data;
using Snt.Romashka.Contracts;
using Snt.Romashka.Database.Core;
using Snt.Romashka.Db.Core.Abstracts;

namespace Snt.Romashka.Repositories.Tests
{
    public static class Helper
    {
        public static void InsertUser(IDbConnection connection, User user, IDbTransaction tran = null)
        {
            var query = @"insert into [user] (user_id, login, email, created, is_active, fio) 
            values(@userId, @login, @email, @created, @isActive, @fio)";

            connection.ExecuteNonQuery(query, user, tran);
        }
        
        public static void InsertUser(ICommonDb commonDb, User user)
        {
            var query = @"insert into [user] (user_id, login, email, created, is_active, fio) 
            values(@userId, @login, @email, @created, @isActive, @fio)";

            commonDb.ExecuteNonQuery(query, user);
        }
    }
}