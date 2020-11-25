using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuizManager.Data.Factories;

namespace QuizManager.UI.Controllers
{
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
            return View();
        }
    }
}