using MoodBot;

namespace ChatBot.Services
{
    public class NextMessageDecigion
    {
        public static string GetNextMessage(int userId, string lastMessage, string message)
        {
            string result = string.Empty;
            // TODO: Work with Db (all messages text need to save in Db). Add defaultText. Also work with messageCodes.
            switch (message)
            {
                case "/start":
                    result = "What's your mood today?";
                    break;
                case "Save today's mood":
                    result = "What's your mood today?";
                    break;
                default: 
                    result = "What you want to do? Repeat, please :3";
                    break;
            }

            return result;
        }
    }
}
