using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;


namespace MoodBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        TelegramBotClient client { get; }

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
            var token = Environment.GetEnvironmentVariable("TelegramToken");
            client = new TelegramBotClient(token);
        }

        [HttpGet("CheckBot")]
        public async Task<string> CheckBot()
        {
            var me = await client.GetMeAsync();

            return $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
        }
    }
}