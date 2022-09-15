using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Data.Interfaces;
using QuizManager.UI.Services;

namespace QuizManager.UI.Controllers;

[Route("api")]
[ApiController]
[Authorize]
public class ParticipateApiController : ControllerBase
{
	private readonly IGameStateRepository _gameStateRepository;
	private readonly QuizManagerStateService _quizManagerStateService;
	private readonly IQuizRepository _quizRepository;
	private readonly IResponseRepository _responseRepository;

	public ParticipateApiController(IQuizRepository quizRepository, IGameStateRepository gameStateRepository,
		IResponseRepository responseRepository, QuizManagerStateService quizManagerStateService)
	{
		_quizRepository = quizRepository;
		_gameStateRepository = gameStateRepository;
		_responseRepository = responseRepository;
		_quizManagerStateService = quizManagerStateService;
	}

	[HttpGet("gameState/GetCurrentQuestion")]
	public IActionResult GetCurrentQuestionForQuiz(int quizId)
	{
		var question = _gameStateRepository.GetCurrentQuestionForQuiz(quizId);

		return Ok(question);
	}

	[HttpGet("gameState/isAcceptingSubmissions")]
	public IActionResult GetIsAcceptingSubmissions([FromForm] int quizId)
	{
		return Ok(_quizManagerStateService.IsQuizAcceptingSubmissions(quizId));
	}

	[HttpGet("question/participants")]
	public IActionResult GetParticipantsOfQuestion(int questionId)
	{
		var participants = _quizRepository.GetParticipantsOfQuestion(questionId).ToList();
		var responses = _responseRepository.GetResponsesForQuestion(questionId).ToList();
		var usersWhoResponded = responses.Select(response => response.UserId).ToList();

		// Fill in the reset of the Participant object
		// Get all the users who have answered a particular question
		foreach (var participant in participants)
		{
			// Set the questionId
			participant.QuestionId = questionId;

			// check if this participant has answered
			if (usersWhoResponded.Contains(participant.UserId))
			{
				var usersResponses = responses.Where(response => response.UserId == participant.UserId).ToList();
				var latestResponse = usersResponses.OrderByDescending(response => response.Timestamp).First();
				participant.HasAnswered = true;
				participant.LatestAnswerTime = latestResponse.Timestamp;
			}
		}

		return Ok(participants);
	}

	[HttpGet("quiz/participantScores")]
	public IActionResult GetParticipantScores(int quizId)
	{
		try
		{
			var participantScores = _quizRepository.GetQuizScores(quizId);
			return Ok(participantScores);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("quiz/responses")]
	public IActionResult GetQuizResponses(int quizId)
	{
		// Return error if quiz is still enabled
		if (_quizManagerStateService.IsQuizAcceptingSubmissions(quizId))
		{
			return BadRequest("Quiz still enabled.");
		}

		var responses = _responseRepository.GetResponseItemsForQuiz(quizId)
			.OrderByDescending(item => item.Timestamp);

		return Ok(responses);
	}
}