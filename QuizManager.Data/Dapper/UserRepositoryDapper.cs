using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;

namespace QuizManager.Data.Dapper
{
    public class UserRepositoryDapper : IUserRepository
    {
        public IEnumerable<UserShortItem> GetAllUsers()
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                const string sql =
                    "SELECT Id, UserName AS [name] FROM AspNetUsers";
                return conn.Query<UserShortItem>(sql);
            }
        }
    }
}