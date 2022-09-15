using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces;

public interface IQuestionRepository
{
	int AddQuestion(int quizId, int round, int questionNumber);

	/**
     * Get a question given the question id. Null if not found.
     */
	Question GetQuestion(int questionId);
}