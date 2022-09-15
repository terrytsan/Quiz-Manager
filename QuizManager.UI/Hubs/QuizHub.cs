using Microsoft.AspNetCore.SignalR;
using QuizManager.Data.Interfaces;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;
using QuizManager.UI.Services;

namespace QuizManager.UI.Hubs;

public class QuizHub : Hub
{
	private readonly IGameStateRepository _gameStateRepository;
	private readonly IQuestionRepository _questionRepository;
	private readonly QuizManagerStateService _quizManagerStateService;
	private readonly IQuizRepository _quizRepository;
	private readonly IResponseRepository _responseRepository;
	private readonly IUserRepository _userRepository;

	public QuizHub(IGameStateRepository gameStateRepository, IQuestionRepository questionRepository,
		IQuizRepository quizRepository, IResponseRepository responseRepository, IUserRepository userRepository,
		QuizManagerStateService quizManagerStateService)
	{
		_gameStateRepository = gameStateRepository;
		_questionRepository = questionRepository;
		_quizRepository = quizRepository;
		_responseRepository = responseRepository;
		_userRepository = userRepository;
		_quizManagerStateService = quizManagerStateService;
	}

	public async Task StartQuiz(int quizId)
	{
		// Inform clients to update question
		await Clients.Group("Quiz" + quizId).SendAsync("startQuiz");
	}

	public async Task JoinQuizGroup(int quizId)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, "Quiz" + quizId);
	}

	public void NextQuestion(int quizId)
	{
		// Get the next question
		var nextQuestion = _gameStateRepository.GetNextQuestionForQuiz(quizId);

		if (nextQuestion != null)
		{
			// Update the database gameState
			_gameStateRepository.UpdateCurrentQuestionForQuiz(quizId, nextQuestion.Id);

			// Broadcast updated gameState to clients
			Clients.Group("Quiz" + quizId).SendAsync("advanceQuestion", quizId, nextQuestion);
		}
		else
		{
			// If no more questions, show alert
			Clients.Group("Quiz" + quizId).SendAsync("showEndOfQuizAlert");
		}
	}

	public void PrevQuestion(int quizId)
	{
		// Get the previous question
		var prevQuestion = _gameStateRepository.GetPreviousQuestionForQuiz(quizId);

		// Check if it's the start of the quiz
		if (prevQuestion != null)
		{
			// Update the database gameState
			_gameStateRepository.UpdateCurrentQuestionForQuiz(quizId, prevQuestion.Id);

			// Broadcast updated gameState to clients
			Clients.Group("Quiz" + quizId).SendAsync("advanceQuestion", quizId, prevQuestion);
		}
	}

	/**
     * Starts a countdown on all clients. length is in milliseconds
     */
	public void StartTimer(int quizId, int? length)
	{
		Clients.Group("Quiz" + quizId).SendAsync("startCountdown", length);
	}

	/**
     * Sets the state of the quiz (accepting submissions or not)
     * isAcceptingSubmissions is true if accepting submissions
     */
	public void SetState(int quizId, bool isAcceptingSubmissions)
	{
		_quizManagerStateService.SetState(quizId, isAcceptingSubmissions);
		Clients.Group("Quiz" + quizId).SendAsync("handleQuizStateChange", isAcceptingSubmissions);
	}

	/**
     * Update the points of a response
     */
	public void UpdateResponsePoints(int quizId, int responseId, int points)
	{
		_responseRepository.UpdateResponsePoints(responseId, points);

		// Broadcast singular updated response's score
		Clients.Group("Quiz" + quizId).SendAsync("updateResponseScore", responseId, points);

		// Broadcast the new scores for the quiz
		var participantScores = _quizRepository.GetQuizScores(quizId);
		Clients.Group("Quiz" + quizId).SendAsync("updateParticipantScores", participantScores);
	}

	public bool SubmitResponse(int questionId, string responseText, int quizId)
	{
		if (!_quizManagerStateService.IsQuizAcceptingSubmissions(quizId))
		{
			return false;
		}

		// Add to database
		var response = new Response
		{
			UserId = Context.UserIdentifier,
			QuestionId = questionId,
			ResponseText = responseText,
			Points = 0,
			Timestamp = DateTime.Now
		};

		var responseId = _responseRepository.AddResponse(response);
		var question = _questionRepository.GetQuestion(questionId);

		// Broadcast who just submitted a response
		Clients.Group("Quiz" + quizId).SendAsync("handleNewResponseSubmission", new ResponseItem
		{
			Id = responseId,
			QuizId = question.QuizId,
			UserId = Context.UserIdentifier,
			QuestionId = questionId,
			Round = question.Round,
			QuestionNumber = question.QuestionNumber,
			Name = Context.User.Identity.Name,
			ResponseText = response.ResponseText,
			Points = response.Points,
			Timestamp = response.Timestamp
		});

		return true;
	}
}