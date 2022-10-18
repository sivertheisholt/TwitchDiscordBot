using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchDiscordBot.Entities
{
    public class Command
    {
        public int Level { get; set; }
        public string CommandName { get; set; }
        public string CommandTrigger {get; set;}
        public string CommandTriggerParam { get; set; }
    }
}