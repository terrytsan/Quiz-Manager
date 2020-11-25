using System.Collections.Generic;
using QuizManager.Models.Queries;

namespace QuizManager.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserShortItem> GetAllUsers();
    }
}