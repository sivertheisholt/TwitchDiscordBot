using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces
{
    public interface ICommandUnlockService
    {
        List<Command> GetUnlockedCommands(int level);
        Task UnlockCommands(User user);
    }
}