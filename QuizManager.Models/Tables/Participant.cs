namespace QuizManager.Models
{
    public class Participant
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool HasAnswered { get; set; }
    }
}