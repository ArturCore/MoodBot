using Microsoft.EntityFrameworkCore;
using MoodBot.Models.Db;

namespace MoodBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TelegramUser> Users => Set<TelegramUser>();
        public DbSet<LastMessage> LastMessages => Set<LastMessage>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=moodbot.db");
        }

        internal TelegramUser AddUser(long botUserId)
        {
            using(ApplicationContext context = new ApplicationContext())
            {
                TelegramUser user = new TelegramUser
                {
                    BotUserId = botUserId
                };
                context.Users.Add(user);
                context.SaveChanges();

                return context.Users.FirstOrDefault(u => u.BotUserId == botUserId);
            }
        }

        internal TelegramUser GetUserById(long botUserId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Users.FirstOrDefault(u => u.BotUserId == botUserId);
            }
        }

        internal string? GetLastMessageCode(int userId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                LastMessage lastMessage = context.LastMessages.FirstOrDefault(lm => lm.UserId == userId);
                return lastMessage == null ? string.Empty : lastMessage.MessageCode;
            }
        }

        internal void AddOrUpdateLastMessage(int userId, string messageCode)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                LastMessage curLastMessage = context.LastMessages.FirstOrDefault(lm => lm.UserId == userId);
                if(curLastMessage == null)
                {
                    LastMessage newLastMessage = new LastMessage
                    {
                        MessageCode = messageCode,
                        UserId = userId
                    };
                    context.LastMessages.Add(newLastMessage);
                    context.SaveChanges();
                }
                else
                {
                    curLastMessage.MessageCode = messageCode;
                    context.Update(curLastMessage);
                    context.SaveChanges();
                }                    
            }
        }
    }
}
