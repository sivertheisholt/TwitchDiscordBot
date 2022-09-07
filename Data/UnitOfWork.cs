using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Data.Repositories;
using HuskyBot.Interfaces;
using HuskyBot.Interfaces.IRepositories;

namespace HuskyBot.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository userRepository => new UserRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanged()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}