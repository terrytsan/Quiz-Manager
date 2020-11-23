using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper
{
    public class ResponseRepositoryDapper : IResponseRepository
    {
        public void AddResponse(Response response)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new
                {
                    userId = response.UserId, questionId = response.QuestionId, responseText = response.ResponseText,
                    points = response.Points, timenow = response.Timestamp
                };
                const string sql =
                    "INSERT INTO response (userId, questionId, responseText, points, [timestamp]) VALUES (@userId, @questionId, @responseText, @points, @timenow)";
                conn.Execute(sql, parameters);
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
    }
}