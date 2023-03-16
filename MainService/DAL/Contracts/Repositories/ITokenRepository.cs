using DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.Repositories
{
    public interface ITokenRepository
    {
        void Create(UserRefreshToken token);
        void Update(UserRefreshToken token);
        /*void Delete(UserRefreshToken token);*/
    }
}
