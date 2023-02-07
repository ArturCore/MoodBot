using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using MoodBot.Services;
using MoodBot.Models.Db;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace MoodBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private ApplicationContext _appContext;
        private BotService botService;

        public BotController(ILogger<BotController> logger, IServiceProvider serviceProvider)
        {
            // UNDONE: Do you need to call method CheckBot first for StartReceiving? 
            _logger = logger;
            _appContext = serviceProvider.GetRequiredService<ApplicationContext>();

            botService = serviceProvider.GetRequiredService<BotService>();
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botService.GetBotClient().StartReceiving(
                updateHandler: botService.HandleUpdateAsync,
                pollingErrorHandler: botService.HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        [HttpGet("CheckBot")]
        public async Task<IActionResult> CheckBot()
        {
            try
            {
                var me = await botService.GetBotClient().GetMeAsync();
                return Ok($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}