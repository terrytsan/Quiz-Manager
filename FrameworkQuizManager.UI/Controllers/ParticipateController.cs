using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkQuizManager.Data.Factories;
using FrameworkQuizManager.UI.Models;
using Microsoft.AspNet.Identity;

namespace FrameworkQuizManager.UI.Controllers
{
	[Authorize]
	public class ParticipateController : Controller
	{
		// GET: Participate
		public ActionResult Index()
		{
			var model = new ParticipateViewModel();
			var quizRepo = QuizRepositoryFactory.GetRepository();

			var userId = User.Identity.GetUserId();
			ViewBag.UserId = userId;
			var availableQuizzes = quizRepo.GetQuizzesForParticipant(userId);

			model.AvailableQuizzes = new List<SelectListItem>
			{
				// Add dummy value
				new SelectListItem { Text = "Select a Quiz", Value = "0", Disabled = true, Selected = true }
			};
			foreach (var quiz in availableQuizzes)
			{
				model.AvailableQuizzes.Add(new SelectListItem { Text = quiz.Name, Value = quiz.Id.ToString() });
			}

			model.SelectedQuiz = 0;

			return View(model);
		}
	}
}