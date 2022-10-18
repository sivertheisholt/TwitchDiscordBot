using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchDiscordBot.DTOs
{
    public class TwitchChatMessageDto
    {
        public string Username { get; set; } = "";
        public string Message { get; set; } = "";
    }
}