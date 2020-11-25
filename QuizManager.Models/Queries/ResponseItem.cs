﻿using System;

namespace QuizManager.Models.Queries
{
    public class ResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ResponseText { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }
    }
}