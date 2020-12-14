using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuizManager.Data.Factories;
using QuizManager.UI.Models;

namespace QuizManager.UI.Controllers
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
            model.Responses = responseRepo.GetResponseItemsForQuiz(quizId).OrderByDescending(item => item.Timestamp);

            model.IsAcceptingSubmissions = AppHelperFunctions.IsQuizAcceptingSubmissions(quizId);

            return View(model);
        }
    }
}