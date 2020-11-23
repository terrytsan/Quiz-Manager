using System;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Factories
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