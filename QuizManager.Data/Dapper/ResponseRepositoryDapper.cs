using Dapper;
using QuizManager.Data.Context;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper;

public class ResponseRepositoryDapper : IResponseRepository
{
	private readonly DapperContext _context;

	public ResponseRepositoryDapper(DapperContext context)
	{
		_context = context;
	}

	public int AddResponse(Response response)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new
			{
				userId = response.UserId, questionId = response.QuestionId, responseText = response.ResponseText,
				points = response.Points, timenow = response.Timestamp
			};
			const string sql =
				"INSERT INTO response (userId, questionId, responseText, points, [timestamp]) OUTPUT INSERTED.id VALUES (@userId, @questionId, @responseText, @points, @timenow)";
			return conn.QuerySingle<int>(sql, parameters);
		}
	}

	public IEnumerable<Response> GetResponsesForQuestion(int questionId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { questionId = questionId };
			const string sql =
				"SELECT id, userId, questionId, responseText, points, timestamp FROM response WHERE questionId=@questionId";
			return conn.Query<Response>(sql, parameters);
		}
	}

	public IEnumerable<ResponseItem> GetResponseItemsForQuiz(int quizId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { quizId = quizId };
			const string sql =
				"SELECT r2.id AS id, quiz.id AS quizId, userId, q.id AS questionId, round, questionNumber, UserName AS [name], responseText, points, timestamp FROM quiz JOIN question q ON quiz.id = q.quizId JOIN response r2 ON q.id = r2.questionId join AspNetUsers ANU on r2.userId = ANU.Id WHERE quiz.id=@quizId ORDER BY timestamp";
			return conn.Query<ResponseItem>(sql, parameters);
		}
	}

	public void UpdateResponsePoints(int responseId, int points)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { responseId = responseId, points = points };
			const string sql = "UPDATE response SET points=@points WHERE id=@responseId";
			conn.Execute(sql, parameters);
		}
	}
}