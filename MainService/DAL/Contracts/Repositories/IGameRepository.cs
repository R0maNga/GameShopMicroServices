﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Repositories
{
    public interface IGameRepository
    {
        void Create(Game game);
        void Update(Game game);
        void Delete(Game game);
    }
}
