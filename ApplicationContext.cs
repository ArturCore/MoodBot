using Microsoft.EntityFrameworkCore;
using MoodBot.Models.Db;

namespace MoodBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=moodbot.db");
        }

        internal User AddUser(string botId)
        {
            using(ApplicationContext context = new ApplicationContext())
            {
                User user = new User
                {
                    BotId = botId
                };
                context.Users.Add(user);
                context.SaveChanges();

                return context.Users.FirstOrDefault(u => u.BotId == botId);
            }
        }
    }
}
