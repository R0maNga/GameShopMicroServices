
using BLL.Models.Output.UserOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.UserInput;
using System.IdentityModel.Tokens.Jwt;
using BLL.Models.Output.RefreshTokenOutput;

namespace BLL.Services.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticateResponse?> GetTokenAsync(AuthenticateRequest authenticateRequest, string ipAddress, CancellationToken token);
        Task<AuthenticateResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName, CancellationToken token);
        JwtSecurityToken GetJwtToken(string expiredToken);
        UserRefreshTokenOutput CheckToken(RefreshTokenRequest request);
        public Task UpdateToken(UserRefreshTokenOutput request, CancellationToken token);
        AuthenticateResponse ValidateDetails(JwtSecurityToken token, UserRefreshTokenOutput userRefreshToken);
    }
}
