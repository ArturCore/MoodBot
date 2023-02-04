using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using MoodBot.Services;
using MoodBot.Models.Db;
using Microsoft.Extensions.DependencyInjection;

namespace MoodBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private ApplicationContext _appContext;

        public BotController(ILogger<BotController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _appContext = serviceProvider.GetRequiredService<ApplicationContext>();
        }

        [HttpGet("CheckBot")]
        public async Task<IActionResult> CheckBot()
        {
            try
            {
                BotService bot = new BotService();
                var me = await bot.GetBotClient().GetMeAsync();
                return Ok($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(string botId)
        {
            try
            {
                return Ok(_appContext.AddUser(botId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}