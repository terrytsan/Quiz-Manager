using System.Collections.Generic;
using QuizManager.Models;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IQuizRepository
    {
        // Get a a list of quizzes the user is a participant of 
        IEnumerable<Quiz> GetQuizzesForParticipant(string userId);

        IEnumerable<Participant> GetParticipantsOfQuiz(int questionId);

        // Get a list of quizzes where the user is the host
        IEnumerable<Quiz> GetQuizzesForUser(string userId);

        int AddQuiz(Quiz quiz);
    }
}