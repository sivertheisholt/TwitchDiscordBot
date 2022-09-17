using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HuskyBot.DTOs;
using HuskyBot.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HuskyBot.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            var user = await _unitOfWork.userRepository.GetUser(username);
            if(user == null)
            {
                return NotFound();
            } else {
                return Ok(_mapper.Map<UserDto>(user));
            }
        }
    }
}