using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace MoodBot.Lists
{
    public class ReplyMarkups
    {
        public static IReplyMarkup GetReplyMarkup(string messageCode)
        {
            //string[,] buttons;
            KeyboardButton[][] keyboardButtons = new KeyboardButton[][] { };
            switch (messageCode)
            {
                case "moodQuestion":
                case "start":
                    keyboardButtons = new KeyboardButton[][]
                    {
                        new KeyboardButton[]{ "Set my mood" },
                        new KeyboardButton[]{ "Cancel" } 
                    };
                    break;
            }

            ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardButtons)
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
        }
    }
}
