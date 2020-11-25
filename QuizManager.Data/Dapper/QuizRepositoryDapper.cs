﻿using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using QuizManager.Data.Interfaces;
using QuizManager.Models;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Dapper
{
    public class QuizRepositoryDapper : IQuizRepository
    {
        public Quiz GetQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "SELECT id, hostId, name, startDate FROM quiz WHERE id=@quizId";
                return conn.QuerySingleOrDefault<Quiz>(sql, parameters);
            }
        }

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

        public IEnumerable<Participant> GetParticipantsOfQuestion(int questionId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {questionId = questionId};
                const string sql =
                    "SELECT QuizId, UserId, UserName AS name FROM AspNetUsers_quiz JOIN AspNetUsers ANU ON AspNetUsers_quiz.UserId = ANU.Id WHERE QuizId=(SELECT TOP 1 quizId FROM question where id=@questionId)";
                return conn.Query<Participant>(sql, parameters);
            }
        }

        public IEnumerable<Participant> GetParticipantsOfQuiz(int quizId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {quizId = quizId};
                const string sql =
                    "SELECT QuizId, UserId, UserName AS name FROM AspNetUsers_quiz JOIN AspNetUsers ANU ON AspNetUsers_quiz.UserId = ANU.Id WHERE QuizId=@quizId";
                return conn.Query<Participant>(sql, parameters);
            }
        }

        public IEnumerable<Quiz> GetQuizzesForUser(string userId)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new {userId = userId};
                const string sql = "SELECT id, hostId, name, startDate From quiz where hostId=@userId";
                return conn.Query<Quiz>(sql, parameters);
            }
        }

        public int AddQuiz(Quiz quiz)
        {
            using (var conn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new
                {
                    hostId = quiz.HostId, quizName = quiz.Name, startDate = quiz.StartDate,
                };
                const string sql =
                    "INSERT INTO quiz (hostId, [name], startDate) OUTPUT INSERTED.Id VALUES (@hostId, @quizName, @startDate)";
                return conn.QuerySingle<int>(sql, parameters);
            }
        }
    }
}