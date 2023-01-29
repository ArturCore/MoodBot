using ChatBot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace MoodBot.Services
{
    public class BotService
    {
        TelegramBotClient client { get; }
        public BotService()
        {
            var token = Environment.GetEnvironmentVariable("TelegramToken");
            client = new TelegramBotClient(token);
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

            var chatId = message.Chat.Id;

            string answerMessage = NextMessageDecigion.GetNextMessage(messageText);

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
