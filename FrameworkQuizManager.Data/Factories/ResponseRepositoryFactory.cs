using System;
using FrameworkQuizManager.Data.Dapper;
using FrameworkQuizManager.Data.Interfaces;

namespace FrameworkQuizManager.Data.Factories
{
	public class ResponseRepositoryFactory
	{
		public static IResponseRepository GetRepository()
		{
			switch (Settings.GetRepositoryType())
			{
				case "Dapper":
					return new ResponseRepositoryDapper();
				default:
					throw new Exception("Could not find valid RepositoryType configuration value.");
			}
		}
	}
}