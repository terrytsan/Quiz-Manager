using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Dapper
{
    public class QuestionRepositoryDapper : IQuestionRepository
    {
        public int AddQuestion(int quizId, int round, int questionNumber)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
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
    }
}