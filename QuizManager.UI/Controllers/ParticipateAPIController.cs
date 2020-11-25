﻿using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using QuizManager.Data.Factories;
using QuizManager.Models.Tables;

namespace QuizManager.UI.Controllers
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

        [Route("api/question/participants")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetParticipantsOfQuestion(int questionId)
        {
            var quizRepo = QuizRepositoryFactory.GetRepository();
            var responseRepo = ResponseRepositoryFactory.GetRepository();

            var participants = quizRepo.GetParticipantsOfQuiz(questionId).ToList();
            var responses = responseRepo.GetResponsesForQuestion(questionId);
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
                    participant.HasAnswered = true;
                }
            }

            return Ok(participants);
        }


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
    }
}