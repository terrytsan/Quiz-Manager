using Dapper;
using QuizManager.Data.Context;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;

namespace QuizManager.Data.Dapper;

public class UserRepositoryDapper : IUserRepository
{
	private readonly DapperContext _context;

	public UserRepositoryDapper(DapperContext context)
	{
		_context = context;
	}

	public IEnumerable<UserShortItem> GetAllUsers()
	{
		using (var conn = _context.CreateConnection())
		{
			const string sql =
				"SELECT Id, UserName AS [name] FROM AspNetUsers";
			return conn.Query<UserShortItem>(sql);
		}
	}
}