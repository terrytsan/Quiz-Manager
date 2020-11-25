using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper
{
    public class GameStateRepositoryDapper : IGameStateRepository
    {
        public Question GetCurrentQuestionForQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "SELECT questionId as id, q.quizId, round, questionNumber FROM gameState JOIN question q ON gameState.questionId = q.id WHERE q.quizId=@quizId";
                return conn.QueryFirstOrDefault<Question>(sql, parameters);
            }
        }

        public void InitializeCurrentQuestionForQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "INSERT INTO gameState (quizId, questionId) VALUES (@quizId, (SELECT TOP 1 id FROM question WHERE quizId=@quizId ORDER BY round, questionNumber))";
                conn.Execute(sql, parameters);
            }
        }

        public void UpdateCurrentQuestionForQuiz(int quizId, int questionId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId, questionId = questionId};
                const string sql =
                    "UPDATE gameState SET questionId=@questionId WHERE quizId=@quizId";
                conn.Execute(sql, parameters);
            }
        }

        public Question GetNextQuestionForQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "SELECT id, quizId, round, questionNumber FROM (SELECT * FROM question WHERE quizId=@quizId) AS [q*] WHERE id > (SELECT questionId FROM gameState WHERE quizId=@quizId) ORDER BY round, questionNumber";
                return conn.QueryFirstOrDefault<Question>(sql, parameters);
            }
        }
    }
}