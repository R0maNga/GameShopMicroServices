using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Finders
{
    public interface IUserFinder

    {
        public Task<List<User>> GetAsync(CancellationToken token);
        public Task<User> GetByIdAsync(int id, CancellationToken token);

    }
}
