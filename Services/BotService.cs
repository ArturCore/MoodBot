using ChatBot.Services;
using MoodBot.Models.Db;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

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
                // UNDONE: you have a problem here. If user isn't inside the table TelegramUser - you will catch an exception
                TelegramUser user = _appContext.GetUserById(chatId);
                userId = user != null ? user.Id : _appContext.AddUser(chatId).Id;
            }

            string lastMessage = _appContext.GetLastMessageCode(userId);

            string answerMessage = NextMessageDecigion.GetNextMessage(userId, lastMessage, messageText);
            // TODO: add new lastMessage to Db. Don't add it if answer if default.

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: answerMessage,
                cancellationToken: cancellationToken);
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
