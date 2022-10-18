using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces.IServices
{
    public interface ICommandService
    {
        void HandleCommand(User user, string commandTrigger, string commandTriggerParam, string commandMessage);
    }
}