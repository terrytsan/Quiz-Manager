using System;
using FrameworkQuizManager.Data.Dapper;
using FrameworkQuizManager.Data.Interfaces;

namespace FrameworkQuizManager.Data.Factories
{
	public class GameStateRepositoryFactory
	{
		public static IGameStateRepository GetRepository()
		{
			switch (Settings.GetRepositoryType())
			{
				case "Dapper":
					return new GameStateRepositoryDapper();
				default:
					throw new Exception("Could not find valid RepositoryType configuration value.");
			}
		}
	}
}