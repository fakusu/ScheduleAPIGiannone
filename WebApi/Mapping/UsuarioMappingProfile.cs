using Application.Dtos.Usuario;
using AutoMapper;
using Entities;

namespace WebApi.Mapping
{
    public class UsuarioMappingProfile:Profile

    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>();
               
            CreateMap<UsuarioRequestDto, Usuario>();
        }
    }
}
