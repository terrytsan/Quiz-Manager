using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper
{
    public class ResponseRepositoryDapper : IResponseRepository
    {
        public int AddResponse(Response response)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
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
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {questionId = questionId};
                const string sql =
                    "SELECT id, userId, questionId, responseText, points, timestamp FROM response WHERE questionId=@questionId";
                return conn.Query<Response>(sql, parameters);
            }
        }

        public IEnumerable<ResponseItem> GetResponseItemsForQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "SELECT r2.id, quiz.id, round, questionNumber, UserName AS [name], responseText, points, timestamp FROM quiz JOIN question q ON quiz.id = q.quizId JOIN response r2 ON q.id = r2.questionId join AspNetUsers ANU on r2.userId = ANU.Id WHERE quiz.id=@quizId ORDER BY timestamp";
                return conn.Query<ResponseItem>(sql, parameters);
            }
        }
    }
}