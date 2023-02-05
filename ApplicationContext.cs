using Microsoft.EntityFrameworkCore;
using MoodBot.Models.Db;

namespace MoodBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TelegramUser> Users => Set<TelegramUser>();
        public DbSet<Message> Messages => Set<Message>();
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

        internal string GetLastMessageCode(int userId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                int messageId = context.LastMessages.FirstOrDefault(lm => lm.UserId == userId).MessageId;
                return context.Messages.FirstOrDefault(m => m.MessageId == messageId).MessageCode;
            }
        }

        internal void AddLastMessageCode(int userId, string messageCode)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                int messageId = context.Messages.FirstOrDefault(m => m.MessageCode == messageCode).MessageId;
                LastMessage lastMessage = new LastMessage
                {
                    MessageId = messageId,
                    UserId = userId
                };
                context.LastMessages.Add(lastMessage);
                context.SaveChanges();
            }
        }
    }
}
