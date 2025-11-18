using Application.Dtos.MetaUsuario;
using AutoMapper;
using Entities;

namespace WebApi.Mapping
{
    public class MetaUsuarioMappingProfile:Profile
    {
        public MetaUsuarioMappingProfile()
        {
            CreateMap<MetaUsuario, MetaUsuarioResponseDto>();
            CreateMap<MetaUsuarioRequestDto, MetaUsuario>();
        }
    }
}
