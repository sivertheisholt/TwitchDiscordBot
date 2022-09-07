using Discord;
using Discord.WebSocket;
using HuskyBot.Entities;
using HuskyBot.Interfaces.IServices;

namespace HuskyBot.Services
{
 public class DiscordBotService : IDiscordBotService
    {
        private DiscordSocketClient _client;
        private Task _ready;
        private readonly IConfiguration _config;
        private readonly string _twitch_chat_id;
        private readonly string _twitch_bot_id;
        private readonly string _token;
        private readonly string _discord_twitch_emote_id;
        

        public DiscordBotService(IConfiguration config)
        {
            _config = config;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                _twitch_chat_id = _config.GetSection("DISCORD_APP")["DISCORD_CHANNEL_TWITCH_CHAT_ID"];
                _twitch_bot_id = _config.GetSection("DISCORD_APP")["DISCORD_CHANNEL_TWITCH_BOT_ID"];
                _token = _config.GetSection("DISCORD_APP")["DISCORD_CLIENT_TOKEN"];
                _discord_twitch_emote_id = _config.GetSection("DISCORD_APP")["DISCORD_TWITCH_EMOTE_ID"];
            }
            else
            {
                _twitch_chat_id = Environment.GetEnvironmentVariable("DISCORD_CHANNEL_TWITCH_CHAT_ID");
                _twitch_bot_id = Environment.GetEnvironmentVariable("DISCORD_CHANNEL_TWITCH_BOT_ID");
                _token = Environment.GetEnvironmentVariable("DISCORD_CLIENT_TOKEN");
                _discord_twitch_emote_id = Environment.GetEnvironmentVariable("DISCORD_TWITCH_EMOTE_ID");
            }
            
            Thread trd = new Thread(new ThreadStart(this.InitClient));
            trd.Start();
        }

        private async void InitClient()
        {
            _client = new DiscordSocketClient();
            _client.Ready += ReadyAsync;

            await _client.LoginAsync(TokenType.Bot, _token);
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
            var channel = await _client.GetChannelAsync(Convert.ToUInt64(_twitch_chat_id)) as IMessageChannel;
            await channel.SendMessageAsync($"<{_discord_twitch_emote_id}> " + msg);
        }
        public async Task SendMessageCount(int messagecount)
        {
            throw new NotImplementedException();
        }
        public async Task SendLevelUpMessage(User user, string message)
        {
            var eb = new EmbedBuilder();

            eb.Title = $"Twitch Level Up!";
            eb.Description = $"Congrats {user.Username} you just leveled up to {user.Level}!\nTheir level up message was: \n{message}";
            eb.AddField("Level", user.Level, true);
            eb.AddField("Messages", user.Messagecount, true);
            eb.AddField("XP", user.Xp, true);
            eb.AddField("You can find them over at", "https://www.twitch.tv/" + user.Username, false);
            eb.ThumbnailUrl = "https://i.imgur.com/wjfQpwo.png";
            eb.Color = 0x00FFFF;
            eb.Footer = new EmbedFooterBuilder(){
                Text = "Level up by participating in the chat and unlock cool stuff!"
            };

            var channel = await _client.GetChannelAsync(Convert.ToUInt64(_twitch_bot_id)) as IMessageChannel;
            await channel.SendMessageAsync(embed: eb.Build());
        }
    }
}