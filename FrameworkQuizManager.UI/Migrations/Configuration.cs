using System.Data.Entity.Migrations;
using FrameworkQuizManager.UI.Models;

namespace FrameworkQuizManager.UI.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}
	}
}