using System;
using FrameworkQuizManager.Data.Dapper;
using FrameworkQuizManager.Data.Interfaces;

namespace FrameworkQuizManager.Data.Factories
{
	public class QuestionRepositoryFactory
	{
		public static IQuestionRepository GetRepository()
		{
			switch (Settings.GetRepositoryType())
			{
				case "Dapper":
					return new QuestionRepositoryDapper();
				default:
					throw new Exception("Could not find valid RepositoryType configuration value.");
			}
		}
	}
}