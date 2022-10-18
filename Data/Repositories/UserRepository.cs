using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;
using TwitchDiscordBot.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace TwitchDiscordBot.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> CheckIfUserExists(string username)
        {
            return await Context.User.AnyAsync(x => x.Username == username);
        }

        public async Task<User> GetUser(string username)
        {
            return await Context.User.FirstOrDefaultAsync(user => user.Username == username);
        }

        public void NewUser(User user)
        {
            Context.User.Add(user);
        }
    }
}