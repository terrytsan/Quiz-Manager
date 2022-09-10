using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FrameworkQuizManager.Models.Queries;
using FrameworkQuizManager.Models.Tables;

namespace FrameworkQuizManager.UI.Models
{
	public class ParticipateViewModel
	{
		public List<SelectListItem> AvailableQuizzes { get; set; }

		[Required(ErrorMessage = "Please select a quiz.")]
		[Display(Name = "Quiz")]
		public int SelectedQuiz { get; set; }

		[Required] public string Response { get; set; }

		public Question CurrentQuestion { get; set; }

		public IEnumerable<QuestionParticipant> Participants { get; set; }

		public IEnumerable<QuizParticipant> QuizParticipants { get; set; }
	}
}