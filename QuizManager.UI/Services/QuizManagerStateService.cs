namespace QuizManager.UI.Services;

public class QuizManagerStateService
{
	public QuizManagerStateService()
	{
		quizStatus = new Dictionary<int, bool>();
	}

	/**
     * Holds state of each quiz (accepting submissions or not)
     * Key: QuizID
	 * Value: isAcceptingSubmissions - is true if accepting submissions
     */
	private static Dictionary<int, bool> quizStatus { get; set; }

	/**
	 * Returns status of quiz.
	 * If quiz status isn't found, initialise to false and return false.
	 */
	public bool IsQuizAcceptingSubmissions(int quizId)
	{
		if (quizStatus.ContainsKey(quizId))
		{
			return quizStatus[quizId];
		}

		quizStatus[quizId] = false;
		return false;
	}

	public void SetState(int quizId, bool isAcceptingSubmissions)
	{
		quizStatus[quizId] = isAcceptingSubmissions;
	}
}