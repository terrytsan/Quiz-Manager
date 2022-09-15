using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizManager.Data.Interfaces;
using QuizManager.UI.Models;

namespace QuizManager.UI.Controllers;

[Authorize]
public class ParticipateController : Controller
{
	private readonly IQuizRepository _quizRepository;

	public ParticipateController(IQuizRepository quizRepository)
	{
		_quizRepository = quizRepository;
	}

	public IActionResult Index()
	{
		var model = new ParticipateViewModel();

		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		ViewBag.UserId = userId;
		var availableQuizzes = _quizRepository.GetQuizzesForParticipant(userId);

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