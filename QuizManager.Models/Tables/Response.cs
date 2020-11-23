using System;

namespace QuizManager.Models.Tables
{
    public class Response
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public string ResponseText { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }
    }
}