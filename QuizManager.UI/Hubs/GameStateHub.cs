using Microsoft.AspNet.SignalR;
using QuizManager.Data.Factories;

namespace QuizManager.UI.Hubs
{
    public class GameStateHub : Hub
    {
        public void NextQuestion(int quizId)
        {
            var gameStateRepo = GameStateRepositoryFactory.GetRepository();

            // Get the next question number
            var nextQuestion = gameStateRepo.GetNextQuestionForQuiz(quizId);

            if (nextQuestion != null)
            {
                // Update the database gameState
                gameStateRepo.UpdateCurrentQuestionForQuiz(quizId, nextQuestion.Id);

                // Broadcast updated gameState to clients
                Clients.All.updateCurrentQuestionText(nextQuestion);
            }
            else
            {
                // If no more questions, show alert
                Clients.All.showEndOfQuizAlert();
            }
        }
    }
}