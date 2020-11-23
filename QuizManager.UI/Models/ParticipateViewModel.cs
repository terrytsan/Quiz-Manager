using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizManager.Models;
using QuizManager.Models.Tables;

namespace QuizManager.UI.Models
{
    public class ParticipateViewModel
    {
        
        public List<SelectListItem> AvailableQuizzes { get; set; }
        
        [Display(Name = "Quiz")]
        public int SelectedQuiz { get; set; }
        
        public string Response { get; set; }
        
        public Question CurrentQuestion { get; set; }
        
        public IEnumerable<Participant> Participants { get; set; }
    }
}