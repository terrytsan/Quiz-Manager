using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuizManager.Data.Factories;
using QuizManager.UI.Models;

namespace QuizManager.UI.Controllers
{
    public class ParticipateController : Controller
    {
        // GET: Participate
        public ActionResult Index()
        {
            var model = new ParticipateViewModel();
            var quizRepo = QuizRepositoryFactory.GetRepository();
            
            var availableQuizzes = quizRepo.GetQuizzesForParticipant(User.Identity.GetUserId());
            
            model.AvailableQuizzes = new List<SelectListItem>
            {
                // Add dummy value
                new SelectListItem {Text = "Select a Quiz", Value = "0", Disabled = true, Selected = true}
            };
            foreach (var quiz in availableQuizzes)
            {
                model.AvailableQuizzes.Add(new SelectListItem{Text=quiz.Name, Value=quiz.Id.ToString()});
            }
            model.SelectedQuiz = 0;
            
            return View(model);
        }
    }
}