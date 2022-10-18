using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Interfaces.IRepositories;

namespace TwitchDiscordBot.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository{get;}

        Task<bool> Complete();
        bool HasChanged();
    }
}