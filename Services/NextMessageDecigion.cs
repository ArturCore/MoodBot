using MoodBot;
using MoodBot.Lists;

namespace ChatBot.Services
{
    public class NextMessageDecigion
    {

        public static string GetNextMessageCode(string? lastBotMessage, string message)
        {
            string resultCode = string.Empty;
            bool isResultAllow;
            //if it's first user message
            if (lastBotMessage == null)
            {
                return resultCode = "moodQuestion";
            }
            else
            {
                //allow list of next message for every messageCode
                bool allowAllMessages = false;
                List<string> allowMessages = new List<string>();
                switch (lastBotMessage)
                {
                    case "moodQuestion":
                        allowMessages.AddRange(new[] {
                            "blushMood"
                    });
                        break;
                    case "thanksGoodbye":
                        allowMessages.AddRange(new[] {
                            "changeRemind",
                            "moodQuestion"
                    });
                        break;
                    default:
                        allowAllMessages = true;
                        break;
                }

                //choose next messageCode
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
                    case "changeRemind":
                        resultCode = "inDevelopment";
                        break;
                }
                if (resultCode == string.Empty)
                {
                    resultCode = "default";
                }
                
                if(!allowAllMessages)
                {
                    isResultAllow = allowMessages.FirstOrDefault(am => am == resultCode) != null ? true : false;
                    if (!isResultAllow)
                    {
                        resultCode = "default";
                    }
                    else
                    {
                        resultCode = "lastBotMessage";
                    }
                }                
            }
            return resultCode;
        }        
    }
}
