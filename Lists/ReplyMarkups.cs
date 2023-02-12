using Telegram.Bot.Types.ReplyMarkups;

namespace MoodBot.Lists
{
    public class ReplyMarkups
    {
        public static IReplyMarkup GetReplyMarkup(string message)
        {
            string[,] buttons;
            switch (message)
            {
                case "start":
                    buttons = new string [,] { { "Set my mood" }, { "Cancel" } };
                    break;
            }

            // TODO: form dynamic keyboards from array buttons
            KeyboardButton[][] keyboardButtons = new KeyboardButton[][]
            {
                new KeyboardButton[] { "Cancel22" }
            };

            ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardButtons)
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
        }
    }
}
