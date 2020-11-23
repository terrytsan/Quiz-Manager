
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace QuizManager.UI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<QuizManager.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}