using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchDiscordBot.DTOs
{
    public class EvilInsultDto
    {
        public int Number { get; set; }
        public string Language { get; set; }
        public string Insult { get; set; }
        public string Created { get; set; }
        public string Shown { get; set; }
        public string Createdby { get; set; }
        public string Active { get; set; }
        public string Comment { get; set; }
    }
}