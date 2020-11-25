using System;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Factories
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