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

        public TwitchChatController(ILogger<TwitchChatController> logger, IUnitOfWork unitOfWork, IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
            _unitOfWork = unitOfWork;
            _logger = logger;    
        }

        [HttpPost]
        [Route("message")]
        public async Task<IActionResult> NewMessage(TwitchChatMessageDto twitchChatMessageDto)
        {
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
            await _userMessageService.HandleNewMessage(user, twitchChatMessageDto.Username, twitchChatMessageDto.Message);
            return Ok();
        } 
    }
}