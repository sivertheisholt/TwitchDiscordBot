using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;

namespace HuskyBot.Interfaces
{
    public interface ICommandUnlockService
    {
        List<Command> GetUnlockedCommands(int level);
        Task UnlockCommands(User user);
    }
}