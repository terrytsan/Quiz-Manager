using System;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Factories
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