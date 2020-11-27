namespace QuizManager.Models.Queries
{
    /**
     * Participant in the context for the whole quiz
     */
    public class QuizParticipant
    {
        public string UserId { get; set; }

        public int QuizId { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }
    }
}