
using BLL.Models.Output.UserOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.UserInput;

namespace BLL.Services.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticateResponse> GetTokenAsync(AuthenticateRequest authenticateRequest, string ipAdress);
        Task<AuthenticateResponse> GetRefreshTokenAsync(string ipAdress, int userId, string userName);
        
    }
}
