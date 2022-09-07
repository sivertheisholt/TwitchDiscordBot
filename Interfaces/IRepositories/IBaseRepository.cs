using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskyBot.Interfaces.IRepositories
{
    public interface IBaseRepository<in T> where T: class
    {
        public void Delete(T entity);
        public void Update(T entity);
    }
}