using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.UserOutput;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL;
using DAL.Entityes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly MainServiceContext _context;
        private readonly IGetTokenBytes _getToken;

        public TokenService(MainServiceContext context, IGetTokenBytes getToken)
        {
            _context = context;
            _getToken = getToken;

        }

        
        public async Task<AuthenticateResponse> GetTokenAsync(AuthenticateRequest authenticateRequest, string ipAdress)
        {
            var user = _context.UserEntities.FirstOrDefault(x => x.UserName.Equals(authenticateRequest.Username)
                                                                 && x.Password.Equals(authenticateRequest.Password));
            if (user ==null)
            {
                return await Task.FromResult<AuthenticateResponse>(null);
            }

            
            string tokenString =  GenerateToken(user.UserName);
            string refreshToken = GenerateRefreshToken();
            await SaveTokenDetails(ipAdress, refreshToken, tokenString, user.Id);
            return new AuthenticateResponse { Token = tokenString, RefreshToken = refreshToken};
        }

        private async Task<AuthenticateResponse> SaveTokenDetails(string ipAdress, string refreshToken, string tokenString, int userId)
        {
            var userRefreshToken = new UserRefreshToken
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.UtcNow.AddMinutes(3),
                IpAdress = ipAdress,
                IsInvalidated = false,
                RefreshToken = refreshToken,
                Token = tokenString,
                Id = userId
            };
            await _context.UserRefreshTokens.AddAsync(userRefreshToken);
            await _context.SaveChangesAsync();
            return new AuthenticateResponse { Token = tokenString, RefreshToken = refreshToken, IsSuccess = true};
        }

        public async Task<AuthenticateResponse> GetRefreshTokenAsync(string ipAdress, int userId, string userName)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(userName);
            return await SaveTokenDetails(ipAdress, refreshToken, accessToken, userId );

        }

        public Task<bool> IsTokenValid(string accessToken, string ipAdress)
        {
            throw new NotImplementedException();
        }

        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            using (var cryptoProvide = new RNGCryptoServiceProvider())
            {
                cryptoProvide.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }

        private  string GenerateToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();


            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_getToken.GetTokeBytes()),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
