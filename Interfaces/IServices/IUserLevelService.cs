using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;

namespace HuskyBot.Interfaces.IServices
{
    public interface IUserLevelService
    {
        bool CheckLevel(User user);
        List<double> GetLevels();
    }
}