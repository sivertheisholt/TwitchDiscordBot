using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchDiscordBot.DTOs;
using TwitchDiscordBot.Interfaces.IServices;

namespace TwitchDiscordBot.Services
{
    public class TwitchService : ITwitchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _twitchBotBaseUrl;
        private readonly string _twitchBotBasePort;
        private readonly IConfiguration _config;
        public TwitchService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                _twitchBotBaseUrl = _config.GetSection("TWITCH_BOT")["TWITCH_BOT_BASEURL"];
                _twitchBotBasePort = _config.GetSection("TWITCH_BOT")["TWITCH_BOT_BASEPORT"];
            }
            else
            {
                _twitchBotBaseUrl = Environment.GetEnvironmentVariable("TWITCH_BOT_BASEURL");
                _twitchBotBasePort = Environment.GetEnvironmentVariable("TWITCH_BOT_BASEPORT");
            }
        }
        public async void SendLevelUp(TwitchlevelUpMessageDto twitchLevelUpMessageDto)
        {
            Console.WriteLine("POST: Send level up message");
            var jsonString = JsonSerializer.Serialize(twitchLevelUpMessageDto);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try{
                await _httpClient.PostAsync($"{_twitchBotBaseUrl}:{_twitchBotBasePort}/twitch/levelup", httpContent);
            } catch(HttpRequestException e) {
                Console.WriteLine("Can't connect to API, skipping..." + e);
            }
        }
        public async void SendChatMessage(TwitchMessageDto messageDto)
        {
            Console.WriteLine("POST: Send chat message");
            var jsonString = JsonSerializer.Serialize(messageDto);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try{
                await _httpClient.PostAsync($"{_twitchBotBaseUrl}:{_twitchBotBasePort}/twitch/message", httpContent);
            } catch(HttpRequestException e) {
                Console.WriteLine("Can't connect to API, skipping..." + e);
            }
        }
    }
}