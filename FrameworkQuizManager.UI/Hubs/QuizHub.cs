using System;
using System.Collections.Generic;
using FrameworkQuizManager.Data.Factories;
using FrameworkQuizManager.Models.Queries;
using FrameworkQuizManager.Models.Tables;
using FrameworkQuizManager.UI.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace FrameworkQuizManager.UI.Hubs
{
	public class QuizHub : Hub
	{
		public void StartQuiz(int quizId)
		{
			// Inform clients to update question
			Clients.Group("Quiz" + quizId).startQuiz();
		}

		public void JoinQuizGroup(int quizId)
		{
			Groups.Add(Context.ConnectionId, "Quiz" + quizId);
		}

		public void NextQuestion(int quizId)
		{
			var gameStateRepo = GameStateRepositoryFactory.GetRepository();

			// Get the next question
			var nextQuestion = gameStateRepo.GetNextQuestionForQuiz(quizId);

			if (nextQuestion != null)
			{
				// Update the database gameState
				gameStateRepo.UpdateCurrentQuestionForQuiz(quizId, nextQuestion.Id);

				// Broadcast updated gameState to clients
				Clients.Group("Quiz" + quizId).advanceQuestion(quizId, nextQuestion);
			}
			else
			{
				// If no more questions, show alert
				Clients.Group("Quiz" + quizId).showEndOfQuizAlert();
			}
		}

		public void PrevQuestion(int quizId)
		{
			var gameStateRepo = GameStateRepositoryFactory.GetRepository();

			// Get the previous question
			var prevQuestion = gameStateRepo.GetPreviousQuestionForQuiz(quizId);

			// Check if it's the start of the quiz
			if (prevQuestion != null)
			{
				// Update the database gameState
				gameStateRepo.UpdateCurrentQuestionForQuiz(quizId, prevQuestion.Id);

				// Broadcast updated gameState to clients
				Clients.Group("Quiz" + quizId).advanceQuestion(quizId, prevQuestion);
			}
		}

		/**
         * Starts a countdown on all clients. length is in milliseconds
         */
		public void StartTimer(int quizId, int length)
		{
			Clients.Group("Quiz" + quizId).startCountdown(length);
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
			((Dictionary<int, bool>)System.Web.HttpContext.Current.Application["QuizStatus"])[quizId] =
				isAcceptingSubmissions;

			Clients.Group("Quiz" + quizId).handleQuizStateChange(isAcceptingSubmissions);
		}

		/**
         * Update the points of a response
         */
		public void UpdateResponsePoints(int quizId, int responseId, int points)
		{
			var responseRepo = ResponseRepositoryFactory.GetRepository();
			var quizRepo = QuizRepositoryFactory.GetRepository();

			responseRepo.UpdateResponsePoints(responseId, points);

			// Broadcast singular updated response's score
			Clients.Group("Quiz" + quizId).updateResponseScore(responseId, points);

			// Broadcast the new scores for the quiz
			var participantScores = quizRepo.GetQuizScores(quizId);
			Clients.Group("Quiz" + quizId).updateParticipantScores(participantScores);
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
			Clients.Group("Quiz" + quizId).handleNewResponseSubmission(new ResponseItem
			{
				Id = responseId, QuizId = question.QuizId, UserId = Context.User.Identity.GetUserId(),
				QuestionId = questionId, Round = question.Round, QuestionNumber = question.QuestionNumber,
				Name = Context.User.Identity.Name, ResponseText = response.ResponseText, Points = response.Points,
				Timestamp = response.Timestamp
			});

			return true;
		}
	}
}