using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IGameStateRepository
    {
        // Get the current question for a particular quiz
        Question GetCurrentQuestionForQuiz(int quizId);
    }
}