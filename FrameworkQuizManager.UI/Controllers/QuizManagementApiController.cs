﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using FrameworkQuizManager.Data.Factories;
using FrameworkQuizManager.Models.Tables;
using FrameworkQuizManager.UI.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace FrameworkQuizManager.UI.Controllers
{
	public class QuizManagementApiController : ApiController
	{
		[Route("api/quizManagement/createQuiz")]
		[AcceptVerbs("POST")]
		public IHttpActionResult CreateQuiz(CreateQuizModel model)
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();
			var questionRepo = QuestionRepositoryFactory.GetRepository();

			var roundInfo = JsonConvert.DeserializeObject<Dictionary<int, int>>(model.RoundInfoString);

			try
			{
				// Create entry in quiz table and get quizID
				var quizId = quizRepo.AddQuiz(new Quiz
					{ HostId = User.Identity.GetUserId(), Name = model.QuizName, StartDate = DateTime.Today });


				// Add questions to database
				foreach (var roundNumber in roundInfo.Keys)
				{
					for (int i = 1; i <= roundInfo[roundNumber]; i++)
					{
						questionRepo.AddQuestion(quizId, roundNumber, i);
					}
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/quizManagement/startQuiz")]
		[AcceptVerbs("POST")]
		public IHttpActionResult StartQuiz([FromBody] int quizId)
		{
			var gameStateRepo = GameStateRepositoryFactory.GetRepository();

			try
			{
				gameStateRepo.InitializeCurrentQuestionForQuiz(quizId);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/quizManagement/addParticipant")]
		[AcceptVerbs("POST")]
		public IHttpActionResult AddParticipant(ParticipantModel model)
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();

			try
			{
				quizRepo.AddParticipantToQuiz(model.QuizId, model.UserId);
				// Return a list of all participants of the quiz
				return Ok(quizRepo.GetParticipantsOfQuiz(model.QuizId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/quizManagement/removeParticipant")]
		[AcceptVerbs("DELETE")]
		public IHttpActionResult RemoveParticipant(ParticipantModel model)
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();

			try
			{
				quizRepo.RemoveParticipantFromQuiz(model.QuizId, model.UserId);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}