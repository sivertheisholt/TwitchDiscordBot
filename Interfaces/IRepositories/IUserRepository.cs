using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces.IRepositories
{
    public interface IUserRepository :IBaseRepository<User>
    {
        void NewUser(User user);
        Task<User> GetUser(string username);
        Task<bool> CheckIfUserExists(string username);
    }
}