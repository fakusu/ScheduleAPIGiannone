using Application.Dtos.Admin.Mes;
using AutoMapper;
using Entities;

namespace WebApi.Mapping.Admin
{
    public class MesMappingProfile : Profile
    {
        public MesMappingProfile()
        {
            CreateMap<Mes, MesResponseDto>().
                ForMember(dest => dest.Nombre, ori => ori.MapFrom(src => src.GetClassName()));
            CreateMap<MesRequestDto, Mes>().ConstructUsing(dto => new Mes(dto.Nombre)); ;
        }
    }
}
