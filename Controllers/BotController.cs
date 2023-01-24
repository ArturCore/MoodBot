using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using MoodBot.Services;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace MoodBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        [HttpGet("CheckBot")]
        public async Task<string> CheckBot()
        {
            BotService bot = new BotService();
            var me = await bot.GetBotClient().GetMeAsync();
            return $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
        }
    }
}