using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;

namespace HuskyBot.Interfaces.IServices
{
    public interface IDiscordBotService
    {
        Task SendTwitchChatMessage(string msg);
        Task SendLevelUpMessage(User user, string message);
    }
}