using System.Collections.Generic;
using FrameworkQuizManager.Models.Queries;

namespace FrameworkQuizManager.Data.Interfaces
{
	public interface IUserRepository
	{
		IEnumerable<UserShortItem> GetAllUsers();
	}
}