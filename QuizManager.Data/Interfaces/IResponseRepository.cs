﻿using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces;

public interface IResponseRepository
{
	int AddResponse(Response response);

	IEnumerable<Response> GetResponsesForQuestion(int questionId);

	/**
     * Gets a list of all responses for the entire quiz (includes name)
     */
	IEnumerable<ResponseItem> GetResponseItemsForQuiz(int quizId);

	/**
     * Update the points for the specified response
     */
	void UpdateResponsePoints(int responseId, int points);
}