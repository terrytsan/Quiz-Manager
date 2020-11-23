using System.Configuration;

namespace QuizManager.Data
{
    public class Settings
    {
        private static string _connectionString;

        private static string _repositoryType;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            return _connectionString;
        }

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_repositoryType))
            {
                _repositoryType = ConfigurationManager.AppSettings["RepositoryType"].ToString();
            }

            return _repositoryType;
        }
    }
}