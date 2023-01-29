namespace ChatBot.Services
{
    public class NextMessageDecigion
    {
        public static string GetNextMessage(string command)
        {
            string result = string.Empty;

            switch (command)
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
