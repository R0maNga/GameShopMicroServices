using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using BLL.Models.Output.UserOutput;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenRequest = MainService.Models.Request.RefreshTokenRequest.RefreshTokenRequest;

namespace MainService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AuthToken([FromBody] AuthenticateRequest authRequest, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticateResponse

                    { IsSuccess = false, Reason = "UserName and Password must be provided" });
            }
            var authResponse = await _tokenService.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress!.ToString(), token);
            if (authResponse is null)
            {
                return Unauthorized();
            }
            
            
            return Ok(authResponse);
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticateResponse { IsSuccess = false, Reason = "Token must be provided" });
            }

            var token = _tokenService.GetJwtToken(request.ExpiredToken);
            var mappedData = _mapper.Map<BLL.Models.Input.UserInput.RefreshTokenRequest>(request);
            var tokenOutput =_tokenService.CheckToken(mappedData);
            AuthenticateResponse response = _tokenService.ValidateDetails(token, tokenOutput);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            tokenOutput.IsInvalidated = true;
            await _tokenService.UpdateToken(tokenOutput, cancellationToken);

            var userName = token.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var authResponse = await _tokenService.GetRefreshTokenAsync(tokenOutput.IpAdress,tokenOutput.Id, userName, cancellationToken);

            return Ok(authResponse);

        }

        

        
    }

}
