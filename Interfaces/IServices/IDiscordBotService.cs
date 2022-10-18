using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces.IServices
{
    public interface IDiscordBotService
    {
        Task SendTwitchChatMessage(string msg);
        Task SendLevelUpMessage(User user, string message, string unlockedCommands);
    }
}