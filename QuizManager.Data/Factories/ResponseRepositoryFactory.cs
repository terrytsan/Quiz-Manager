using System;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.Data.Factories
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