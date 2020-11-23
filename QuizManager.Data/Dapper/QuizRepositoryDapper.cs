using System.Collections.Generic;
using System.Data.SqlClient;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Tables;
using Dapper;
using QuizManager.Models;

namespace QuizManager.Data.Dapper
{
    public class QuizRepositoryDapper : IQuizRepository
    {
        public IEnumerable<Quiz> GetQuizzesForParticipant(string userId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {userId = userId};
                const string sql =
                    "SELECT q.id, hostId, name, startDate FROM AspNetUsers_quiz JOIN quiz q on q.id = AspNetUsers_quiz.QuizId WHERE UserId=@userId ORDER BY startDate";
                return conn.Query<Quiz>(sql, parameters);
            }
        }

        public IEnumerable<Participant> GetParticipantsOfQuiz(int questionId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {questionId = questionId};
                const string sql =
                    "SELECT QuizId, UserId, UserName AS name FROM AspNetUsers_quiz JOIN AspNetUsers ANU ON AspNetUsers_quiz.UserId = ANU.Id WHERE QuizId=(SELECT TOP 1 quizId FROM question where id=@questionId)";
                return conn.Query<Participant>(sql, parameters);
            }
        }
    }
}