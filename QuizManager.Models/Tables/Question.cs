namespace QuizManager.Models.Tables;

public class Question
{
	public int Id { get; set; }
	public int QuizId { get; set; }
	public int Round { get; set; }
	public int QuestionNumber { get; set; }
}