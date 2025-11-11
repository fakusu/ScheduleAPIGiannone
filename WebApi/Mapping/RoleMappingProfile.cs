using Application.Dtos.Identity.Roles;
using AutoMapper;
using Entities.MicrosoftIdentity;

namespace WebApi.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
