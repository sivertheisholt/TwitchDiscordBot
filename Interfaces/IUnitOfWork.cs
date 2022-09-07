using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Interfaces.IRepositories;

namespace HuskyBot.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository{get;}

        Task<bool> Complete();
        bool HasChanged();
    }
}