using System.Collections.Generic;
using FrameworkQuizManager.Models.Queries;
using FrameworkQuizManager.Models.Tables;

namespace FrameworkQuizManager.UI.Models
{
	public class QuizDetailsViewModel
	{
		public Quiz Quiz { get; set; }

		public IEnumerable<QuizParticipant> Participants { get; set; }

		public IEnumerable<UserShortItem> AllUsers { get; set; }

		public Question CurrentQuestion { get; set; }

		public IEnumerable<ResponseItem> Responses { get; set; }

		public bool IsAcceptingSubmissions { get; set; }
	}
}