using ChatBot.Services;
using MoodBot.Models.Db;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using MoodBot.Lists;

namespace MoodBot.Services
{
    public class BotService
    {
        TelegramBotClient client { get; }
        private ApplicationContext _appContext;
        public BotService(IServiceProvider serviceProvider)
        {
            var token = Environment.GetEnvironmentVariable("TelegramToken");
            client = new TelegramBotClient(token);
            _appContext = serviceProvider.GetRequiredService<ApplicationContext>();
        }

        public TelegramBotClient GetBotClient()
        {
            return client;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            long chatId = message.Chat.Id;
            int userId;
            {
                TelegramUser user = _appContext.GetUserById(chatId);
                if (user != null)
                {
                    userId = user.Id;
                }
                else
                {
                    userId = _appContext.AddUser(chatId).Id;
                }
            }

            string? lastMessage = _appContext.GetLastMessageCode(userId);

            string answerMessage = NextMessageDecigion.GetNextMessage(lastMessage, messageText);

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: answerMessage,
                cancellationToken: cancellationToken);

            if(BotMessages.GetDefaultMessage() != answerMessage)
            {
                _appContext.AddOrUpdateLastMessage(userId, answerMessage);
            }
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
