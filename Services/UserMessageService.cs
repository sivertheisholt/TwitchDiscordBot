using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public UserMessageService(IUserLevelService userLevelService, IDiscordBotService discordBotService, IUnitOfWork unitOfWork)
        {
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

        public async Task HandleNewMessage(User user, string username, string message)
        {
            await _discordBotService.SendTwitchChatMessage($"{username}: {message}");
            AddXp(user);
            user.Messagecount++;
            if(_userLevelService.CheckLevel(user))
            {
                await _discordBotService.SendLevelUpMessage(user, message);
            }

            await _unitOfWork.Complete();
        }
    }
}