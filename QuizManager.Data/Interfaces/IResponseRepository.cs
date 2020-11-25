using System.Collections.Generic;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IResponseRepository
    {
        void AddResponse(Response response);

        IEnumerable<Response> GetResponsesForQuestion(int questionId);

        /**
         * Gets a list of all responses for the entire quiz (includes name)
         */
        IEnumerable<ResponseItem> GetResponseItemsForQuiz(int quizId);
    }
}