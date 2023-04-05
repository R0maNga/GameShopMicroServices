using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public abstract class AbstractRepository<T>  where T : class 
    {
        private readonly DbSet<T> _dbSet;

        public AbstractRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
