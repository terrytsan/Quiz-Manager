using System.Collections.Generic;
using QuizManager.Models;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.UI.Models
{
    public class QuizDetailsViewModel
    {
        public Quiz Quiz { get; set; }

        public IEnumerable<Participant> Participants { get; set; }

        public IEnumerable<UserShortItem> AllUsers { get; set; }

        public Question CurrentQuestion { get; set; }

        public IEnumerable<ResponseItem> Responses { get; set; }
    }
}