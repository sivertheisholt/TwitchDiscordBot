using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.DTOs;
using HuskyBot.Entities;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IServices;

namespace HuskyBot.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IUserLevelService _userLevelService;
        private readonly IDiscordBotService _discordBotService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITwitchService _twitchService;
        public UserMessageService(IUserLevelService userLevelService, IDiscordBotService discordBotService, IUnitOfWork unitOfWork, ITwitchService twitchService)
        {
            _twitchService = twitchService;
            _unitOfWork = unitOfWork;
            _discordBotService = discordBotService;
            _userLevelService = userLevelService;
        }

        private void AddXp(User user)
        {
            if(user.Username.Equals("wondyrr"))
            {
                user.Xp += 8;
            } else {
                user.Xp += 2;
            }
        }

        public async Task HandleNewMessage(User user, string username, string message, string twitchName)
        {
            await _discordBotService.SendTwitchChatMessage($"{username}: {message}");
            AddXp(user);
            user.Messagecount++;
            if(_userLevelService.CheckLevel(user))
            {
                await _discordBotService.SendLevelUpMessage(user, message);
                var twitchLevelUpmessageDto = new TwitchlevelUpMessageDto() {Message = $"Congratulation @{user.Username} you just level up to {user.Level}! You have sent {user.Messagecount} and earned {user.Xp}!", TwitchName = twitchName, Username = user.Username};
                _twitchService.SendLevelUp(twitchLevelUpmessageDto);
            }

            await _unitOfWork.Complete();
        }
    }
}