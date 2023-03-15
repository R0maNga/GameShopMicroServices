using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using BLL.Models.Output.UserOutput;
using BLL.Services;
using BLL.Services.Interfaces;
using MainService.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenRequest = MainService.Models.Request.RefreshTokenRequest;

namespace MainService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITokenChecker _tokenChecker;
        private readonly IMapper _mapper;

        public AccountController(ITokenService tokenService, ITokenChecker tokenChecker, IMapper mapper)
        {
            _tokenService = tokenService;
            _tokenChecker = tokenChecker;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AuthToken([FromBody] AuthenticateRequest authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticateResponse
                    { IsSuccess = false, Reason = "UserName and Password must be provided" });
            }
            var authResponse = await _tokenService.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString());
            if (authResponse == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(authResponse);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] MainService.Models.Request.RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticateResponse { IsSuccess = false, Reason = "Token must be provided" });
            }

            var token = GetJwtToken(request.ExpiredToken);
            var mappedData = _mapper.Map<BLL.Models.Input.UserInput.RefreshTokenRequest>(request);
            var tokenOutput =_tokenChecker.CheckToken(mappedData);
            AuthenticateResponse response = ValidateDetails(token, tokenOutput);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            tokenOutput.IsInvalidated = true;
            _tokenChecker.UpdateToken(tokenOutput);

            var userName = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var authResponse = await _tokenService.GetRefreshTokenAsync(tokenOutput.IpAdress,tokenOutput.Id, userName);
            return Ok(authResponse);

        }

        private AuthenticateResponse ValidateDetails(JwtSecurityToken token, UserRefreshTokenOutput userRefreshTOken)
        {
            if (userRefreshTOken==null)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Invalid Token Details" };
            }

            if (token.ValidTo>DateTime.UtcNow)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Token expired" };
            }

            if (!userRefreshTOken.IsActive)
            {
                return new AuthenticateResponse { IsSuccess = false, Reason = "Refresh Token Expired" };
            }

            return new AuthenticateResponse { IsSuccess = true };
        }

        private JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }

}
