using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchDiscordBot.Entities
{
    public class User
    {  
        public int Id { get; set; }
        public string Username { get; set; }
        public int Messagecount { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
        public UserCommand UserCommand { get; set; }

    }
}
