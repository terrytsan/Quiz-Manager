using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using FrameworkQuizManager.Data.Interfaces;
using FrameworkQuizManager.Models.Queries;

namespace FrameworkQuizManager.Data.Dapper
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