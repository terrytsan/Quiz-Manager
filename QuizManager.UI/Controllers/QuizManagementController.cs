using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;
using QuizManager.UI.Models;
using QuizManager.UI.Services;

namespace QuizManager.UI.Controllers;

[Authorize]
public class QuizManagementController : Controller
{
	private readonly IGameStateRepository _gameStateRepository;
	private readonly QuizManagerStateService _quizManagerStateService;
	private readonly IQuizRepository _quizRepository;
	private readonly IResponseRepository _responseRepository;
	private readonly IUserRepository _userRepository;

	public QuizManagementController(IQuizRepository quizRepository, IUserRepository userRepository,
		IGameStateRepository gameStateRepository, IResponseRepository responseRepository,
		QuizManagerStateService quizManagerStateService)
	{
		_quizRepository = quizRepository;
		_userRepository = userRepository;
		_gameStateRepository = gameStateRepository;
		_responseRepository = responseRepository;
		_quizManagerStateService = quizManagerStateService;
	}

	public IActionResult Index()
	{
		var quizzes = _quizRepository.GetQuizzesForUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

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

		model.Quiz = _quizRepository.GetQuiz(quizId);
		model.Participants = _quizRepository.GetQuizScores(quizId);
		model.AllUsers = _userRepository.GetAllUsers();
		model.CurrentQuestion = _gameStateRepository.GetCurrentQuestionForQuiz(quizId);

		var responseItems = _responseRepository.GetResponseItemsForQuiz(quizId)
			.OrderByDescending(item => item.Timestamp).ToList();
		if (!responseItems.Any())
		{
			// Add dummy item if there are no responses
			model.Responses = new[] { new ResponseItem { Id = -1, Name = "Placeholder" } };
		}
		else
		{
			model.Responses = responseItems;
		}

		model.IsAcceptingSubmissions = _quizManagerStateService.IsQuizAcceptingSubmissions(quizId);

		return View(model);
	}
}