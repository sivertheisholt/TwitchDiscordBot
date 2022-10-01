using HuskyBot.DTOs;
using HuskyBot.Entities;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HuskyBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitchChatController : Controller
    {
        private readonly ILogger<TwitchChatController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMessageService _userMessageService;
        private readonly IConfiguration _config;
        private readonly string _twitchName;
        private readonly ICommandService _commandService;

        public TwitchChatController(ILogger<TwitchChatController> logger, IUnitOfWork unitOfWork, IUserMessageService userMessageService, IConfiguration configuration, ICommandService commandService)
        {
            _commandService = commandService;
            _config = configuration;
            _userMessageService = userMessageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                _twitchName = _config.GetSection("TWITCH_BOT")["TWITCH_NAME"];
            }
            else
            {
                _twitchName = Environment.GetEnvironmentVariable("TWITCH_NAME");
            }
        }

        [HttpPost]
        [Route("message")]
        public async Task<IActionResult> NewMessage(TwitchChatMessageDto twitchChatMessageDto)
        {
            Console.WriteLine("Request received for new message");
            
            var user = await _unitOfWork.userRepository.GetUser(twitchChatMessageDto.Username);
            if(user == null)
            {
                Console.WriteLine(twitchChatMessageDto.Username);
                user = new User()
                {
                    Username = twitchChatMessageDto.Username,
                    Level = 0,
                    Messagecount = 1,
                    Xp = 0
                };
                Console.WriteLine("Creating new user");
                _unitOfWork.userRepository.NewUser(user);
                await _unitOfWork.Complete();
            }
            await _userMessageService.HandleNewMessage(user, twitchChatMessageDto.Username, twitchChatMessageDto.Message, _twitchName);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost]
        [Route("command")]
        public async Task<IActionResult> NewCommand(TwitchCommandDto commandDto)
        {
            Console.WriteLine("Request received for new command");
            var user = await _unitOfWork.userRepository.GetUser(commandDto.Username);
            _commandService.HandleCommand(user, commandDto.CommandTrigger, commandDto.CommandTriggerParam, commandDto.CommandMessage);
            return Ok();
        }
    }
}