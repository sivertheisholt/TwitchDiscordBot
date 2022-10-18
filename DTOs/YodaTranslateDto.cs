using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitchDiscordBot.Entities
{
    public class YodaTranslateDto
    {
        [JsonProperty("translation")]
        public string Translation { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translated")]
        public string Translated { get; set; }
    }
}