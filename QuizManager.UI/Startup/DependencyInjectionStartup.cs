using QuizManager.Data.Context;
using QuizManager.Data.Dapper;
using QuizManager.Data.Interfaces;

namespace QuizManager.UI.Startup;

public static class DependencyInjectionStartup
{
	public static IServiceCollection RegisterRepositories(this IServiceCollection services)
	{
		services.AddSingleton<DapperContext>();
		services.AddScoped<IGameStateRepository, GameStateRepositoryDapper>();
		services.AddScoped<IQuestionRepository, QuestionRepositoryDapper>();
		services.AddScoped<IQuizRepository, QuizRepositoryDapper>();
		services.AddScoped<IResponseRepository, ResponseRepositoryDapper>();
		services.AddScoped<IUserRepository, UserRepositoryDapper>();
		return services;
	}
}