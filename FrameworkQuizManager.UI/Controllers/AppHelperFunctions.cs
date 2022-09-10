using System.Collections.Generic;
using System.Web;

namespace FrameworkQuizManager.UI.Controllers
{
	public static class AppHelperFunctions
	{
		/**
         * Returns status of the quiz.
         * Check if status value exists yet. If it does, return status, otherwise, initialise to false and return.
         */
		public static bool IsQuizAcceptingSubmissions(int quizId)
		{
			if (HttpContext.Current.Application["QuizStatus"] == null)
			{
				// Initialize the dictionary
				var quizStatus = new Dictionary<int, bool>();
				HttpContext.Current.Application["QuizStatus"] = quizStatus;
			}
			else
			{
				if (((Dictionary<int, bool>)HttpContext.Current.Application["QuizStatus"]).ContainsKey(quizId))
				{
					// Return actual value
					return ((Dictionary<int, bool>)HttpContext.Current.Application["QuizStatus"])[quizId];
				}
			}

			// Initialize quiz status to false and return 
			((Dictionary<int, bool>)HttpContext.Current.Application["QuizStatus"])[quizId] = false;

			return false;
		}
	}
}