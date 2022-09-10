using System.Linq;
using System.Web.Mvc;
using FrameworkQuizManager.Data.Factories;
using FrameworkQuizManager.Models.Queries;
using FrameworkQuizManager.UI.Models;
using Microsoft.AspNet.Identity;

namespace FrameworkQuizManager.UI.Controllers
{
	[Authorize]
	public class QuizManagementController : Controller
	{
		// GET: QuizManagement
		public ActionResult Index()
		{
			var quizRepo = QuizRepositoryFactory.GetRepository();
			var quizzes = quizRepo.GetQuizzesForUser(User.Identity.GetUserId());

			// Shows a list of their quizzes
			return View(quizzes);
		}

		[HttpGet]
		public ActionResult CreateQuiz()
		{
			return View();
		}

		[HttpGet]
		public ActionResult QuizDetails(int quizId)
		{
			var model = new QuizDetailsViewModel();

			var quizRepo = QuizRepositoryFactory.GetRepository();
			var userRepo = UserRepositoryFactory.GetRepository();
			var gameStateRepo = GameStateRepositoryFactory.GetRepository();
			var responseRepo = ResponseRepositoryFactory.GetRepository();


			model.Quiz = quizRepo.GetQuiz(quizId);
			model.Participants = quizRepo.GetQuizScores(quizId);
			model.AllUsers = userRepo.GetAllUsers();
			model.CurrentQuestion = gameStateRepo.GetCurrentQuestionForQuiz(quizId);

			var responseItems = responseRepo.GetResponseItemsForQuiz(quizId).OrderByDescending(item => item.Timestamp);
			if (!responseItems.Any())
			{
				// Add dummy item if there are no responses
				model.Responses = new[] { new ResponseItem { Id = -1, Name = "Placeholder" } };
			}
			else
			{
				model.Responses = responseItems;
			}

			model.IsAcceptingSubmissions = AppHelperFunctions.IsQuizAcceptingSubmissions(quizId);

			return View(model);
		}
	}
}