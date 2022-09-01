using HuskyBot.DTOs;
using HuskyBot.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HuskyBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitchChatController : Controller
    {
        
        private readonly ILogger<TwitchChatController> _logger;
        private readonly IDiscordBotService _discordBotService;

        public TwitchChatController(ILogger<TwitchChatController> logger, IDiscordBotService discordBotService)
        {
            _logger = logger;
            _discordBotService = discordBotService;        
        }

        [HttpPost]
        [Route("message")]
        public IActionResult NewMessage(TwitchChatMessageDto twitchChatMessageDto)
        {
            _discordBotService.SendTwitchChatMessage($"{twitchChatMessageDto.Username}: {twitchChatMessageDto.Message}");

            return Ok();
        } 
    }
}