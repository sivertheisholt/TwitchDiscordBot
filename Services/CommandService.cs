using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchDiscordBot.Api;
using TwitchDiscordBot.DTOs;
using TwitchDiscordBot.Entities;
using TwitchDiscordBot.Interfaces;
using TwitchDiscordBot.Interfaces.IServices;
using TwitchDiscordBot.Utilities;

namespace TwitchDiscordBot.Services
{
    public class CommandService : ICommandService
    {
        private readonly ITwitchService _twitchService;
        private readonly FunTranslationApi _funTranslationApi;
        private readonly ICommandUnlockService _commandUnlockService;
        private readonly IConfiguration _config;
        private readonly string _twitchName;
        private readonly EvilInsultApi _evilInsultApi;
        public CommandService(IConfiguration config, ITwitchService twitchService, FunTranslationApi funTranslationApi, ICommandUnlockService commandUnlockService, EvilInsultApi evilInsultApi) 
        {
            _evilInsultApi = evilInsultApi;
            _config = config;
            _commandUnlockService = commandUnlockService;
            _funTranslationApi = funTranslationApi;
            _twitchService = twitchService;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                _twitchName = _config.GetSection("TWITCH_BOT")["TWITCH_NAME"];
            }
            else
            {
                _twitchName = Environment.GetEnvironmentVariable("TWITCH_NAME");
            }
        }
        public async void HandleCommand(User user, string commandTrigger, string commandTriggerParam, string commandMessage)
        {
            commandTrigger = HelperUtils.ToUpperFirstLetter(commandTrigger);
            commandTriggerParam = HelperUtils.ToUpperFirstLetter(commandTriggerParam);
            var commandName = commandTrigger + commandTriggerParam;
            if(user.UserCommand == null) await _commandUnlockService.UnlockCommands(user);
            try
            {
                if( (bool) user.UserCommand.GetType().GetProperty(commandName).GetValue(user.UserCommand))
                {
                    switch (commandName)
                    {
                        case "TranslateYoda":
                            var res = await _funTranslationApi.TranslateYoda(commandMessage);
                            var dto = new TwitchMessageDto() {Message = res.Translated, TwitchName = _twitchName};
                            _twitchService.SendChatMessage(dto);
                            break;
                        case "Insult":
                            var resInsult = await _evilInsultApi.GetInsult();
                            var dtoInsult = new TwitchMessageDto() {Message = resInsult.Insult, TwitchName = _twitchName};
                            _twitchService.SendChatMessage(dtoInsult);
                            break;
                        default:
                            break;
                    }
                } else {
                    var dtoInsult = new TwitchMessageDto() {Message = "I'm sorry, but you are not allowed to use this command!", TwitchName = _twitchName};
                    _twitchService.SendChatMessage(dtoInsult);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Something wrong happen handeling command... " + e);
            }
            
        }
    }
}