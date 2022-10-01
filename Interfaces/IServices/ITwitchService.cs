using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.DTOs;

namespace HuskyBot.Interfaces.IServices
{
    public interface ITwitchService
    {
        void SendLevelUp(TwitchlevelUpMessageDto twitchLevelUpMessageDto);
        void SendChatMessage(TwitchMessageDto messageDto);
    }
}