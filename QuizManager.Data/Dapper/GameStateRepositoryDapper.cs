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
    }
}