using System.Collections.Generic;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IResponseRepository
    {
        void AddResponse(Response response);

        IEnumerable<Response> GetResponsesForQuestion(int questionId);
    }
}