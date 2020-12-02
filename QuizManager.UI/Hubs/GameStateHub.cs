using System.Collections.Generic;
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
                Clients.All.advanceQuestion(quizId, nextQuestion);
            }
            else
            {
                // If no more questions, show alert
                Clients.All.showEndOfQuizAlert();
            }
        }

        /**
         * Starts a countdown on all clients. length is in milliseconds
         */
        public void StartTimer(int quizId, int length)
        {
            Clients.All.startCountdown(quizId, length);
        }

        /**
         * Sets the state of the quiz (accepting submissions or not)
         * isAcceptingSubmissions is true if accepting submissions
         */
        public void SetState(int quizId, bool isAcceptingSubmissions)
        {
            // Check if the state exists
            if (System.Web.HttpContext.Current.Application["QuizStatus"] == null)
            {
                System.Web.HttpContext.Current.Application["QuizStatus"] = new Dictionary<int, bool>();
            }

            // Set the value 
            ((Dictionary<int, bool>) System.Web.HttpContext.Current.Application["QuizStatus"])[quizId] =
                isAcceptingSubmissions;

            Clients.All.handleQuizStateChange(quizId, isAcceptingSubmissions);
        }
    }
}