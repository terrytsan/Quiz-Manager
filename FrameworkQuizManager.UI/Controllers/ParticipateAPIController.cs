using System;
using System.Linq;
using System.Web.Http;
using FrameworkQuizManager.Data.Factories;
using FrameworkQuizManager.Models.Tables;
using Microsoft.AspNet.Identity;

namespace FrameworkQuizManager.UI.Controllers
{
	public class ParticipateApiController : ApiController
	{
		[Route("api/gameState/GetCurrentQuestion")]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetCurrentQuestionForQuiz(int quizId)
		{
			var gameStateRepo = GameStateRepositoryFactory.GetRepository();

			var question = gameStateRepo.GetCurrentQuestionForQuiz(quizId);

			return Ok(question);
		}

		[Route("api/gameState/isAcceptingSubmissions")]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetIsAcceptingSubmissions(int quizId)
		{
			return Ok(AppHelperFunctions.IsQuizAcceptingSubmissions(quizId));
		}

		[Route("api/question/participants")]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetParticipantsOfQuestion(int questionId)
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();
			var responseRepo = ResponseRepositoryFactory.GetRepository();

			var participants = quizRepo.GetParticipantsOfQuestion(questionId).ToList();
			var responses = responseRepo.GetResponsesForQuestion(questionId).ToList();
			var usersWhoResponded = responses.Select(response => response.UserId).ToList();

			// Fill in the reset of the Participant object
			// Get all the users who have answered a particular question
			foreach (var participant in participants)
			{
				// Set the questionId
				participant.QuestionId = questionId;

				// check if this participant has answered
				if (usersWhoResponded.Contains(participant.UserId))
				{
					var usersResponses = responses.Where(response => response.UserId == participant.UserId).ToList();
					var latestResponse = usersResponses.OrderByDescending(response => response.Timestamp).First();
					participant.HasAnswered = true;
					participant.LatestAnswerTime = latestResponse.Timestamp;
				}
			}

			return Ok(participants);
		}


		/**
         * Deprecated. Replaced with SignalR hub function
         */
		[Route("api/participate/submit")]
		[AcceptVerbs("POST")]
		public IHttpActionResult SubmitResponse(Response submittedResponse)
		{
			var responseRepo = ResponseRepositoryFactory.GetRepository();
			var response = new Response
			{
				UserId = User.Identity.GetUserId(), QuestionId = submittedResponse.QuestionId,
				ResponseText = submittedResponse.ResponseText, Timestamp = DateTime.Now
			};

			try
			{
				responseRepo.AddResponse(response);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/quiz/participantScores")]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetParticipantScores(int quizId)
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();

			try
			{
				var participantScores = quizRepo.GetQuizScores(quizId);
				return Ok(participantScores);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/quiz/responses")]
		[AcceptVerbs("GET")]
		public IHttpActionResult GetQuizResponses(int quizId)
		{
			// Return error if quiz is still enabled
			if (AppHelperFunctions.IsQuizAcceptingSubmissions(quizId))
			{
				return BadRequest("Quiz still enabled.");
			}

			var responseRepo = ResponseRepositoryFactory.GetRepository();

			var responses = responseRepo.GetResponseItemsForQuiz(quizId).OrderByDescending(item => item.Timestamp);

			return Ok(responses);
		}
	}
}