using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Tables;
using QuizManager.UI.Models;

namespace QuizManager.UI.Controllers;

[Route("api/quizManagement")]
[ApiController]
[Authorize]
public class QuizManagementApiController : ControllerBase
{
	private readonly IGameStateRepository _gameStateRepository;
	private readonly IQuestionRepository _questionRepository;
	private readonly IQuizRepository _quizRepository;

	public QuizManagementApiController(IQuizRepository quizRepository, IQuestionRepository questionRepository,
		IGameStateRepository gameStateRepository)
	{
		_quizRepository = quizRepository;
		_questionRepository = questionRepository;
		_gameStateRepository = gameStateRepository;
	}

	[HttpPost("createQuiz")]
	public IActionResult CreateQuiz([FromForm] CreateQuizModel model)
	{
		var roundInfo = JsonSerializer.Deserialize<Dictionary<int, int>>(model.RoundInfoString,
			new JsonSerializerOptions { NumberHandling = JsonNumberHandling.AllowReadingFromString });

		try
		{
			// Create entry in quiz table and get quizID
			var quizId = _quizRepository.AddQuiz(new Quiz
			{
				HostId = User.FindFirstValue(ClaimTypes.NameIdentifier), Name = model.QuizName,
				StartDate = DateTime.Today
			});


			// Add questions to database
			foreach (var roundNumber in roundInfo.Keys)
			{
				for (int i = 1; i <= roundInfo[roundNumber]; i++)
				{
					_questionRepository.AddQuestion(quizId, roundNumber, i);
				}
			}

			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("startQuiz")]
	public IActionResult StartQuiz([FromForm] int quizId)
	{
		try
		{
			_gameStateRepository.InitializeCurrentQuestionForQuiz(quizId);
			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("addParticipant")]
	public IActionResult AddParticipant([FromForm] ParticipantModel model)
	{
		try
		{
			_quizRepository.AddParticipantToQuiz(model.QuizId, model.UserId);
			// Return a list of all participants of the quiz
			return Ok(_quizRepository.GetParticipantsOfQuiz(model.QuizId));
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("removeParticipant")]
	public IActionResult RemoveParticipant([FromForm] ParticipantModel model)
	{
		try
		{
			_quizRepository.RemoveParticipantFromQuiz(model.QuizId, model.UserId);
			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}