using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IGameStateRepository
    {
        // Get the current question for a particular quiz
        Question GetCurrentQuestionForQuiz(int quizId);

        /**
         * Initializes the quiz with an entry in the gameState table (Round 1, Question 1)
         */
        void InitializeCurrentQuestionForQuiz(int quizId);

        /**
         * Sets the current question for a quiz
         */
        void UpdateCurrentQuestionForQuiz(int quizId, int questionId);

        /**
         * Gets the next question for quiz. Returns null if end of quiz
         */
        Question GetNextQuestionForQuiz(int quizId);

        /**
         * Gets the previous question for quiz. Returns null if start of quiz
         */
        Question GetPreviousQuestionForQuiz(int quizId);
    }
}