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
        private readonly ITwitchService _twitchService;
        private readonly ICommandUnlockService _unlockService;
        public UserMessageService(IUserLevelService userLevelService, IDiscordBotService discordBotService, ITwitchService twitchService, ICommandUnlockService unlockService)
        {
            _unlockService = unlockService;
            _twitchService = twitchService;
            _discordBotService = discordBotService;
            _userLevelService = userLevelService;
        }

        private void AddXp(User user)
        {
            if(user.Username.Equals("Wondyrr"))
            {
                user.Xp += 10;
            } else {
                user.Xp += 2;
            }
        }

        public async Task HandleNewMessage(User user, string username, string message, string twitchName)
        {
            await _discordBotService.SendTwitchChatMessage($"{username}: {message}");
            user.Messagecount++;
            AddXp(user);
        
            if(_userLevelService.CheckLevel(user))
            {
                var unlockedCommandsStr = HandleUnlockedCommands(user);
                await _discordBotService.SendLevelUpMessage(user, message, unlockedCommandsStr);
                var twitchLevelUpmessageDto = new TwitchlevelUpMessageDto() {Message = $"Congratulation @{user.Username} you just leveled up to {user.Level}! You have sent {user.Messagecount} messages and earned {user.Xp} XP! You unlocked: \n {unlockedCommandsStr}", TwitchName = twitchName, Username = user.Username};
                _twitchService.SendLevelUp(twitchLevelUpmessageDto);
            }
        }
        private string HandleUnlockedCommands(User user)
        {
            var unlockedCommands = _unlockService.GetUnlockedCommands(user.Level);
            var unlockedCommandsStr = "";
            if(unlockedCommands != null)
            {
                unlockedCommands.ForEach(command => {
                    unlockedCommandsStr += $"!{command.CommandTrigger} {command.CommandTriggerParam} \n";
                    user.UserCommand.GetType().GetProperty(command.CommandName).SetValue(user.UserCommand, true);
                });
            }
            return unlockedCommandsStr;
        }
    }
}