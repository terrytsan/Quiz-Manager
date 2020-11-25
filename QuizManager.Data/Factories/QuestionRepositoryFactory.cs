using System;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Factories
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