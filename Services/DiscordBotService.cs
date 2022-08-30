using Discord;
using Discord.WebSocket;
using HuskyBot.Interfaces.IServices;

namespace HuskyBot.Services
{
 public class DiscordBotService : IDiscordBotService
    {
        private DiscordSocketClient _client;
        private Task _ready;
        private readonly IConfiguration _config;

        private static ulong _huskyGuildId = 277449687777148928;

        public DiscordBotService(IConfiguration config)
        {
            _config = config;
            Thread trd = new Thread(new ThreadStart(this.InitClient));
            trd.IsBackground = true;
            trd.Start();
        }

        private async void InitClient()
        {
            _client = new DiscordSocketClient();
            _client.Ready += ReadyAsync;

            // Login and connect.
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var token = "";
            if (env == "Development")
            {
                token = _config.GetSection("DISCORD_APP")["DISCORD_CLIENT_TOKEN"];
            }
            else
            {
                token = Environment.GetEnvironmentVariable("DISCORD_CLIENT_TOKEN");
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }
        private async Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            _ready = Task.CompletedTask;
        }
        public async Task SendTwitchChatMessage(string msg) 
        {
            var channel = await _client.GetChannelAsync(1014193913868861540) as IMessageChannel;
            await channel.SendMessageAsync(":twitch: " + msg);
        }
    }
}