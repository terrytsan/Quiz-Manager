using Dapper;
using QuizManager.Data.Context;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper;

public class QuizRepositoryDapper : IQuizRepository
{
	private readonly DapperContext _context;

	public QuizRepositoryDapper(DapperContext context)
	{
		_context = context;
	}

	public Quiz GetQuiz(int quizId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { quizId = quizId };
			const string sql =
				"SELECT id, hostId, name, startDate FROM quiz WHERE id=@quizId";
			return conn.QuerySingleOrDefault<Quiz>(sql, parameters);
		}
	}

	public IEnumerable<Quiz> GetQuizzesForParticipant(string userId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { userId = userId };
			const string sql =
				"SELECT q.id, hostId, name, startDate FROM AspNetUsers_quiz JOIN quiz q on q.id = AspNetUsers_quiz.QuizId WHERE UserId=@userId ORDER BY startDate";
			return conn.Query<Quiz>(sql, parameters);
		}
	}

	public IEnumerable<QuestionParticipant> GetParticipantsOfQuestion(int questionId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { questionId = questionId };
			const string sql =
				"SELECT QuizId, UserId, UserName AS name FROM AspNetUsers_quiz JOIN AspNetUsers ANU ON AspNetUsers_quiz.UserId = ANU.Id WHERE QuizId=(SELECT TOP 1 quizId FROM question where id=@questionId)";
			return conn.Query<QuestionParticipant>(sql, parameters);
		}
	}

	public IEnumerable<QuestionParticipant> GetParticipantsOfQuiz(int quizId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { quizId = quizId };
			const string sql =
				"SELECT QuizId, UserId, UserName AS name FROM AspNetUsers_quiz JOIN AspNetUsers ANU ON AspNetUsers_quiz.UserId = ANU.Id WHERE QuizId=@quizId";
			return conn.Query<QuestionParticipant>(sql, parameters);
		}
	}

	public IEnumerable<Quiz> GetQuizzesForUser(string userId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { userId = userId };
			const string sql = "SELECT id, hostId, name, startDate From quiz where hostId=@userId";
			return conn.Query<Quiz>(sql, parameters);
		}
	}

	public int AddQuiz(Quiz quiz)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new
			{
				hostId = quiz.HostId, quizName = quiz.Name, startDate = quiz.StartDate
			};
			const string sql =
				"INSERT INTO quiz (hostId, [name], startDate) OUTPUT INSERTED.Id VALUES (@hostId, @quizName, @startDate)";
			return conn.QuerySingle<int>(sql, parameters);
		}
	}

	public int AddParticipantToQuiz(int quizId, string userId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new
			{
				quizId = quizId, userId = userId
			};
			const string sql =
				"INSERT INTO AspNetUsers_quiz (QuizId, UserId) OUTPUT INSERTED.Id VALUES (@quizId, @userId)";
			return conn.QuerySingle<int>(sql, parameters);
		}
	}

	public QuizParticipant GetParticipantQuizScore(int quizId, string userId)
	{
		using (var conn = _context.CreateConnection())
		{
			var parameters = new { quizId = quizId, userId = userId };

			const string sql = @"SELECT Id AS UserId,
                                        @quizId        as quizId,
                                        UserName as [name],
                                        Score
                                 FROM AspNetUsers
                                        JOIN (SELECT @userId as userId, SUM(Score) as Score
                                            FROM (SELECT MAX(points) AS 'Score'
                                                FROM response
                                                    JOIN question q ON response.questionId = q.id
                                                WHERE quizId = @quizId
                                                  AND userId = @userId
                                                GROUP BY questionId) as highScoreEachQuestion) hs on Id = hs.userId";
			return conn.QueryFirstOrDefault<QuizParticipant>(sql, parameters);
		}
	}

	public IEnumerable<QuizParticipant> GetQuizScores(int quizId)
	{
		// Get a list of participants
		var participants = GetParticipantsOfQuiz(quizId);

		// Get the total scores for the participants
		return participants.Select(participant => GetParticipantQuizScore(quizId, participant.UserId))
			.OrderByDescending(p => p.Score);
	}

	public void RemoveParticipantFromQuiz(int quizId, string userId)
	{
		using (var conn = _context.CreateConnection())
		{
			// Delete from AspNetUsers_quiz (participants table)
			var parameters = new { quizId = quizId, userId = userId };
			const string participantSql =
				"DELETE p FROM AspNetUsers_quiz p WHERE userId=@userId AND QuizId=@quizId ";
			conn.Execute(participantSql, parameters);

			// Delete from response table 
			const string responseSql =
				"DELETE r FROM response r JOIN question q ON r.questionId = q.id WHERE userId=@userId AND quizId=@quizId";
			conn.Execute(responseSql, parameters);
		}
	}
}