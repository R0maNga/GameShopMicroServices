using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using BLL.Models.Output.UserOutput;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL;
using DAL.Contracts.Finders;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IGetTokenBytes _getToken;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenFinder _tokenFinder;
        private readonly IUserFinder _userFinder;

        public TokenService(IGetTokenBytes getToken, IMapper mapper, IHttpContextAccessor httpContext, IUnitOfWork unitOfWork, ITokenRepository tokenRepository, ITokenFinder tokenFinder, IUserFinder userFinder)
        {
           
            _getToken = getToken;
            _mapper = mapper;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
            _tokenRepository = tokenRepository;
            _tokenFinder = tokenFinder;
            _userFinder = userFinder;

        }
        //tyt
        
        public async Task<AuthenticateResponse?> GetTokenAsync(AuthenticateRequest authenticateRequest, string ipAdress, CancellationToken token)
        {
            var user = _userFinder.GetUserByNameAndPassword(authenticateRequest.UserName, authenticateRequest.Password, token);
            if (user is null)
            {
                return null;
            }

            
            var tokenString =  GenerateToken(user.UserName);
            var refreshToken = GenerateRefreshToken();
            await SaveTokenDetails(ipAdress, refreshToken, tokenString, user.Id, token);
            return new AuthenticateResponse { Token = tokenString, RefreshToken = refreshToken};
        }

        //tet
        private async Task<AuthenticateResponse> SaveTokenDetails(string ipAdress, string refreshToken, string tokenString, int userId, CancellationToken token)
        {
            var userRefreshTokenOutput = new UserRefreshTokenOutput
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                IpAdress = ipAdress,
                IsInvalidated = false,
                RefreshToken = refreshToken,
                Token = tokenString,
                Id = userId
            };
            var mappedToken = _mapper.Map<UserRefreshToken>(userRefreshTokenOutput);
            _tokenRepository.Create(mappedToken);
            await _unitOfWork.SaveChanges(token);
            return new AuthenticateResponse { Token = tokenString, RefreshToken = refreshToken, IsSuccess = true};
        }

        public async Task<AuthenticateResponse> GetRefreshTokenAsync(string ipAdress, int userId, string userName, CancellationToken token)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(userName);
            return await SaveTokenDetails(ipAdress, refreshToken, accessToken, userId, token );

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

        public JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }

        
        public UserRefreshTokenOutput CheckToken(RefreshTokenRequest request)
        {
            var ipAdress = _httpContext.HttpContext.Connection.RemoteIpAddress!.ToString();
            var userRefreshToken = _tokenFinder.FindToken(request.RefreshToken, ipAdress, request.ExpiredToken, false);
            
            return _mapper.Map<UserRefreshTokenOutput>(userRefreshToken);
        }
        
        public  Task UpdateToken(UserRefreshTokenOutput request, CancellationToken token)
        {
            var mappedToken = _mapper.Map<UserRefreshToken>(request);
            _tokenRepository.Update(mappedToken);
            return _unitOfWork.SaveChanges(token);

        }

        public AuthenticateResponse ValidateDetails(JwtSecurityToken token, UserRefreshTokenOutput? userRefreshToken)
        {
            if (userRefreshToken is null)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Invalid Token Details" };
            }

            if (!userRefreshToken.IsActive)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Refresh Token Expired" };
            }

            if (token.ValidTo > DateTime.UtcNow)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Token not expired" };
            }

            

            return new AuthenticateResponse { IsSuccess = true };
        }
    }
}
