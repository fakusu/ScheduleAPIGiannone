using Application.Dtos.Admin.Anio;
using AutoMapper;
using Entities;

namespace WebApi.Mapping.Admin
{
    public class AnioMappingProfile : Profile
    {
        public AnioMappingProfile()
        {
            CreateMap<Anio, AnioResponseDto>().
                ForMember(dest => dest.Numero, ori => ori.MapFrom(src => src.GetClassName()));
            CreateMap<AnioRequestDto, Anio>().ConstructUsing(dto => new Anio(dto.Numero));
        }
    }
}
