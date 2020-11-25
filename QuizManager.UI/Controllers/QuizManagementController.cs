using System.Web.Mvc;

namespace QuizManager.UI.Controllers
{
    public class QuizManagementController : Controller
    {
        // GET: QuizManagement
        public ActionResult Index()
        {
            // Shows a list of their quizzes
            return View();
        }

        [HttpGet]
        public ActionResult CreateQuiz()
        {
            return View();
        }

        // [HttpGet]
        // public ActionResult QuizDetails()
        // {
        //     // return View();
        // }
    }
}