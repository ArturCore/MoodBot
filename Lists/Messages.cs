namespace MoodBot.Lists
{
    public class BotMessages
    {
        // <messageCode, messageText>
        public static List<KeyValuePair<string, string>> Messages = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("start", "/start"),
            new KeyValuePair<string, string>("moodQuestion", "What's your mood today?"),
            new KeyValuePair<string, string>("blushMood", "😊"),
            new KeyValuePair<string, string>("thanksGoodbye", "Thanks for your answer! See you tomorrow!"),
            new KeyValuePair<string, string>("default", "What you want to do? Repeat, please :3"),
            new KeyValuePair<string, string>("changeRemind", "Change daily remind"),
            new KeyValuePair<string, string>("inDevelopment", "This section in development")

        };

        public static string GetDefaultMessage()
        {
            return Messages.FirstOrDefault(m => m.Key == "default").Value;
        }

        public static string GetMessageByCode(string messageCode)
        {
            return Messages.FirstOrDefault(m => m.Key == messageCode).Value;
        }
    }
}
