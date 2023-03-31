using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Repositories
{
    public interface IGameStorageRepository
    {
         void Create (GameStorage entity);
         void Update (GameStorage entity);
         void Delete (GameStorage entity);
    }
}
