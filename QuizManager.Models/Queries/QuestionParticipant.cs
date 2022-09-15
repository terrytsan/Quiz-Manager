namespace QuizManager.Models.Queries;

public class QuestionParticipant
{
	public int QuizId { get; set; }
	public int QuestionId { get; set; }
	public string UserId { get; set; }
	public string Name { get; set; }
	public bool HasAnswered { get; set; }
	public DateTime LatestAnswerTime { get; set; }
}