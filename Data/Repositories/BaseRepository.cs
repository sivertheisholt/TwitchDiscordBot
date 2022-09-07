using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuskyBot.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HuskyBot.Data.Repositories
{
    abstract public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        protected DataContext Context { get { return _context; } }
    }
}