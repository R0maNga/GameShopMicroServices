using AutoMapper;
using BLL.Models.Input.UserInput;

namespace MainService.AutoMapper
{
    public class RefreshTokenProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<RefreshTokenRequest, Models.Request.RefreshTokenRequest>();
                CreateMap<Models.Request.RefreshTokenRequest, RefreshTokenRequest>();
            }
        }
    }
}
