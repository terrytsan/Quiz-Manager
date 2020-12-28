using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using QuizManager.Data.Factories;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;
using QuizManager.UI.Controllers;

namespace QuizManager.UI.Hubs
{
    public class ParticipateHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        // Test method to return the calling client's user id
        public string GetOwnUserId()
        {
            return Context.User.Identity.GetUserId();
            // return Clients.Caller.printUserId(Context.User.Identity.Name);
        }

        public bool SubmitResponse(int questionId, string responseText, int quizId)
        {
            var responseRepo = ResponseRepositoryFactory.GetRepository();
            var questionRepo = QuestionRepositoryFactory.GetRepository();

            if (!AppHelperFunctions.IsQuizAcceptingSubmissions(quizId))
            {
                return false;
            }

            // Add to database
            var response = new Response
            {
                UserId = Context.User.Identity.GetUserId(), QuestionId = questionId, ResponseText = responseText,
                Points = 0, Timestamp = DateTime.Now
            };

            var responseId = responseRepo.AddResponse(response);
            var question = questionRepo.GetQuestion(questionId);

            // Broadcast who just submitted a response
            Clients.All.handleNewResponseSubmission(new ResponseItem
            {
                Id = responseId, QuizId = question.QuizId, UserId = Context.User.Identity.GetUserId(),
                QuestionId = questionId, Round = question.Round, QuestionNumber = question.QuestionNumber,
                Name = Context.User.Identity.Name, ResponseText = response.ResponseText, Points = response.Points,
                Timestamp = response.Timestamp, TimestampString = response.Timestamp.ToString("HH:mm:ss.fff")
            });

            return true;
        }

        public void UpdateResponsePoints(int quizId, int responseId, int points)
        {
            var responseRepo = ResponseRepositoryFactory.GetRepository();
            var quizRepo = QuizRepositoryFactory.GetRepository();

            responseRepo.UpdateResponsePoints(responseId, points);

            // Broadcast singular updated response's score
            Clients.All.updateResponseScore(responseId, points);

            // Broadcast the new scores for the quiz
            var participantScores = quizRepo.GetQuizScores(quizId);
            Clients.All.updateParticipantScores(participantScores);
        }
    }
}