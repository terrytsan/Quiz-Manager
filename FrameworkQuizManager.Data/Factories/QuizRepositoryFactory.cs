using System;
using FrameworkQuizManager.Data.Dapper;
using FrameworkQuizManager.Data.Interfaces;

namespace FrameworkQuizManager.Data.Factories
{
	public class QuizRepositoryFactory
	{
		public static IQuizRepository GetRepository()
		{
			switch (Settings.GetRepositoryType())
			{
				case "Dapper":
					return new QuizRepositoryDapper();
				default:
					throw new Exception("Could not find valid RepositoryType configuration value.");
			}
		}
	}
}