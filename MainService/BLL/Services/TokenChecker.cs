using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL;
using DAL.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TokenChecker:ITokenChecker

    {
        private readonly MainServiceContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        /*private readonly DbSet<UserRefreshToken> _dbSet;*/


        public TokenChecker(MainServiceContext context, IHttpContextAccessor httpContext, IMapper mapper /*DbSet<UserRefreshToken> dbSet*/)
        {
            _context = context;
            _httpContext = httpContext;
            _mapper = mapper;
            /*_dbSet = dbSet;*/

        }

        public UserRefreshTokenOutput CheckToken(RefreshTokenRequest request)
        {
            var userRefreshToken =  _context.UserRefreshTokens.AsNoTracking().FirstOrDefault(
                x => x.IsInvalidated == false && x.Token == request.ExpiredToken &&
                     x.RefreshToken == request.RefreshToken
                     && x.IpAdress == _httpContext.HttpContext.Connection.RemoteIpAddress!.ToString());
            return  _mapper.Map<UserRefreshTokenOutput>(userRefreshToken);
        }

        public async void UpdateToken(UserRefreshTokenOutput request)
        {
            /*var res = _dbSet.AsQueryable().AsNoTracking();*/
             _context.UserRefreshTokens.Update(_mapper.Map<UserRefreshToken>(request));
            var sfd = await _context.SaveChangesAsync();
            
        }

        
    }
    
}
