using System;
using FrameworkQuizManager.Data.Dapper;
using FrameworkQuizManager.Data.Interfaces;

namespace FrameworkQuizManager.Data.Factories
{
	public class UserRepositoryFactory
	{
		public static IUserRepository GetRepository()
		{
			switch (Settings.GetRepositoryType())
			{
				case "Dapper":
					return new UserRepositoryDapper();
				default:
					throw new Exception("Could not find valid RepositoryType configuration value.");
			}
		}
	}
}