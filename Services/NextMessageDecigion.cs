using MoodBot;
using MoodBot.Lists;

namespace ChatBot.Services
{
    public class NextMessageDecigion
    {

        public static string GetNextMessage(string? lastMessage, string message)
        {
            string resultCode = string.Empty;
            string messageCode = BotMessages.Messages.FirstOrDefault(m => m.Value == message).Key;
            switch (messageCode)
            {
                case null:
                case "":
                case "start":
                    resultCode = "moodQuestion";
                    break;
                case "blushMood":
                    resultCode = "thanksGoodbye";
                    break;
            }
            if(resultCode == string.Empty)
            {
                resultCode = "default";
            }

            return BotMessages.Messages.FirstOrDefault(m => m.Key == resultCode).Value;
        }        
    }
}
