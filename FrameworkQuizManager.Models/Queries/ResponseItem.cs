using System;

namespace FrameworkQuizManager.Models.Queries
{
	public class ResponseItem
	{
		public int Id { get; set; }
		public int QuizId { get; set; }
		public string UserId { get; set; }
		public int QuestionId { get; set; }
		public int Round { get; set; }
		public int QuestionNumber { get; set; }
		public string Name { get; set; }
		public string ResponseText { get; set; }
		public int Points { get; set; }
		public DateTime Timestamp { get; set; }
	}
}