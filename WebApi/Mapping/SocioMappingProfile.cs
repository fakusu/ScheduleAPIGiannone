using Application.Dtos.Socio;
using AutoMapper;
using Entities;

namespace WebApi.Mapping
{
    public class SocioMappingProfile : Profile
    {
        public SocioMappingProfile()
        {
            CreateMap<Socio, SocioResponseDto>().
                ForMember(dest => dest.FechaNacimiento, ori => ori.MapFrom(src => src.FechaNacimiento.ToShortDateString())).
                ForMember(dest => dest.FechaIngreso, ori => ori.MapFrom(src => src.FechaIngreso.ToShortDateString())).
                ForMember(dest => dest.FechaBaja, ori => ori.MapFrom(src => src.FechaBaja.HasValue ?
                src.FechaBaja.Value.ToShortDateString() : ""));
            CreateMap<SocioRequestDto, Socio>().ConstructUsing(dto => new Socio(dto.Nombre, dto.Apellido, dto.Email));
        }
    }
}
