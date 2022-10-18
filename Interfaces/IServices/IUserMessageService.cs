using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces.IServices
{
    public interface IUserMessageService
    {
        Task HandleNewMessage(User user, string username, string message, string twitchName);
    }
}