using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.DTOs;

namespace TwitchDiscordBot.Interfaces.IServices
{
    public interface ITwitchService
    {
        void SendLevelUp(TwitchlevelUpMessageDto twitchLevelUpMessageDto);
        void SendChatMessage(TwitchMessageDto messageDto);
    }
}