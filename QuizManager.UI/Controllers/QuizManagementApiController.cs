using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using QuizManager.Data.Factories;
using QuizManager.Models.Tables;
using QuizManager.UI.Models;

namespace QuizManager.UI.Controllers
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
                    {HostId = User.Identity.GetUserId(), Name = model.QuizName, StartDate = DateTime.Today});


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
    }
}