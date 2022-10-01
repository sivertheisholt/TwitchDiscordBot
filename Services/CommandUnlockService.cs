using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Entities;
using HuskyBot.Interfaces;
using Newtonsoft.Json;

namespace HuskyBot.Services
{
    
    public class CommandUnlockService : ICommandUnlockService
    {
        private class CommandJson{
            public List<Command> commands;
        }
        private readonly Dictionary<int, List<Command>> _commands;
        public CommandUnlockService()
        {
            _commands = new Dictionary<int, List<Command>>();
            using (StreamReader r = new StreamReader("Commands.json"))
            {
                string json = r.ReadToEnd();
                CommandJson items = JsonConvert.DeserializeObject<CommandJson>(json);
                items.commands.ForEach(item => {
                    if(!_commands.ContainsKey(item.Level))
                    {
                        _commands.Add(item.Level, new List<Command>(){item});
                    } else {
                        _commands[item.Level].Add(item);
                    }
                    
                }); 
            }
        }
        public List<Command> GetUnlockedCommands(int level)
        {
            if(_commands.ContainsKey(level))
            {
                return _commands[level];
            }
            return null;
        }
        public Task UnlockCommands(User user)
        {
            user.UserCommand = new UserCommand {};
            for (int i = user.Level; i >= 0; i--)
            {
                if(_commands.ContainsKey(i))
                {
                    var commands = _commands[i];
                    commands.ForEach(command => {
                        user.UserCommand.GetType().GetProperty(command.CommandName).SetValue(user.UserCommand, true);
                    });
                }
            }
            return Task.CompletedTask;
        }
    }
}