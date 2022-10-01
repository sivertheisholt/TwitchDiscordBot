using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;

namespace HuskyBot.Interfaces.IServices
{
    public interface ICommandService
    {
        void HandleCommand(User user, string commandTrigger, string commandTriggerParam, string commandMessage);
    }
}