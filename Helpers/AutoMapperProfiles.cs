using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TwitchDiscordBot.DTOs;
using TwitchDiscordBot.Entities;

namespace TwitchDiscordBot.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}