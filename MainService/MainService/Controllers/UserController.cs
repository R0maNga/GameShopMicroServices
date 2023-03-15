using System.Security.Claims;
using BLL.Models.Output.UserOutput;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using DAL.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace MainService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MainServiceContext _context;
        private readonly ITokenService _tokenService;
        
        
        
        
           
                
        
    }
}
