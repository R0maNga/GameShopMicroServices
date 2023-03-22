using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public abstract class AbstractRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public AbstractRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public virtual void Create(T item)
        {
            _dbSet.Add(item);
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
        }

        public virtual void Delete(T item)
        {
            _dbSet.Remove(item);
        }

    }
}
