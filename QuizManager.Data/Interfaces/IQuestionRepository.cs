namespace QuizManager.Data.Interfaces
{
    public interface IQuestionRepository
    {
        int AddQuestion(int quizId, int round, int questionNumber);
    }
}