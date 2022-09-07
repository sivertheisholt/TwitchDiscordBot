using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IServices;

namespace HuskyBot.Services
{
    public class UserLevelService : IUserLevelService
    {
        private readonly List<double> _levels = new List<double>();
        private readonly double xpFactor = 1.35;

        public UserLevelService()
        {
            CreateLevels();
        }

        private void CreateLevels()
        {
            var xp = 20.0;
            for(var i = 0; i < 100; i++)
            {
                var newXp = Math.Floor((float) xp * xpFactor);
                _levels.Add(newXp);
                xp = newXp;
            }
        }

        public bool CheckLevel(User user)
        {
            var userLevel = user.Level;
            var userXp = user.Xp;

            if(_levels.ElementAt(userLevel) < userXp)
            {
                user.Level++;
                return true;
            }
            return false;
        }
        public List<double> GetLevels()
        {
            return _levels;
        }

    }
}