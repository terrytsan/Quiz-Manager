using Dapper;
using QuizManager.Data.Context;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper;

public class QuestionRepositoryDapper : IQuestionRepository
{
	private readonly DapperContext _context;

	public QuestionRepositoryDapper(DapperContext context)
	{
		_context = context;
	}

	public int AddQuestion(int quizId, int round, int questionNumber)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new
			{
				quizId = quizId, round = round, questionNumber = questionNumber,
			};
			const string sql =
				"INSERT INTO question (quizId, round, questionNumber) OUTPUT INSERTED.Id VALUES (@quizId, @round, @questionNumber)";
			return conn.QuerySingle<int>(sql, parameters);
		}
	}

	public Question GetQuestion(int questionId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { questionId = questionId };
			const string sql = "SELECT id, quizId, round, questionNumber FROM question where id=@questionId";
			return conn.QuerySingleOrDefault<Question>(sql, parameters);
		}
	}
}