using AutoMapper;
using BLL.Models.Output.RefreshTokenOutput;
using MainService.Models.Response;

namespace MainService.AutoMapper
{
    public class UserRefreshTokenProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<UserRefreshTokenOutput, UserRefreshTokenResponse>();
            }
        }
    }
}
