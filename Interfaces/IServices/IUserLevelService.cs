using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Interfaces.IServices
{
    public interface IUserLevelService
    {
        bool CheckLevel(User user);
        List<double> GetLevels();
    }
}