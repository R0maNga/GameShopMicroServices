using AutoMapper;
using BLL.Models.Input.UserInput;
using MainService.Models.Request.RefreshTokenRequest;
using RefreshTokenRequest = BLL.Models.Input.UserInput.RefreshTokenRequest;

namespace MainService.AutoMapper
{
    public class RefreshTokenProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<RefreshTokenRequest, Models.Request.RefreshTokenRequest.RefreshTokenRequest>();
                CreateMap<Models.Request.RefreshTokenRequest.RefreshTokenRequest,RefreshTokenRequest >();
            }
        }
    }
}
