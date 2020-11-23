using System;

namespace QuizManager.Models.Tables
{
    public class Quiz
    {
        public int Id { get; set; }
        public string HostId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
}