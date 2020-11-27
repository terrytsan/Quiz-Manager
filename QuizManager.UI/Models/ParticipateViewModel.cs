using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.UI.Models
{
    public class ParticipateViewModel
    {
        public List<SelectListItem> AvailableQuizzes { get; set; }

        [Display(Name = "Quiz")] public int SelectedQuiz { get; set; }

        public string Response { get; set; }

        public Question CurrentQuestion { get; set; }

        public IEnumerable<QuestionParticipant> Participants { get; set; }

        public IEnumerable<QuizParticipant> QuizParticipants { get; set; }
    }
}