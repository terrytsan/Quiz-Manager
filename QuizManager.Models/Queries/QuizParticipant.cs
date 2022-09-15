namespace QuizManager.Models.Queries;

public class QuizParticipant
{
	public string UserId { get; set; }

	public int QuizId { get; set; }

	public string Name { get; set; }

	public int Score { get; set; }
}