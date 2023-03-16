using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Finders
{
    public interface ITokenFinder
    {
        UserRefreshToken FindToken(string refreshToken, string ipAdress, string token, bool isInvalidated);
    }
}
