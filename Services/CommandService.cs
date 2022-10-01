using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Api;
using HuskyBot.DTOs;
using HuskyBot.Entities;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IServices;
using HuskyBot.Utilities;

namespace HuskyBot.Services
{
    public class CommandService : ICommandService
    {
        private readonly ITwitchService _twitchService;
        private readonly FunTranslationApi _funTranslationApi;
        private readonly ICommandUnlockService _commandUnlockService;
        private readonly IConfiguration _config;
        private readonly string _twitchName;
        public CommandService(IConfiguration config, ITwitchService twitchService, FunTranslationApi funTranslationApi, ICommandUnlockService commandUnlockService) 
        {
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
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Something wrong happen handeling command...");
            }
            
        }
    }
}