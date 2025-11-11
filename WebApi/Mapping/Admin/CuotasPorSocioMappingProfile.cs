using Application.Dtos.Admin.CuotaPorSocio;
using AutoMapper;
using Entities;

namespace WebApi.Mapping.Admin
{
    public class CuotasPorSocioMappingProfile : Profile
    {
        public CuotasPorSocioMappingProfile()
        {
            CreateMap<CuotaPorSocio, CuotaPorSocioResponseDto>().
                ForMember(dest => dest.FechaPago, ori => ori.MapFrom(src => src.FechaPago.HasValue ?
                src.FechaPago.Value.ToShortDateString() : "")).
                ForMember(dest => dest.Socio, ori => ori.MapFrom(src => src.Socio.GetCompleteName())).
                ForMember(dest => dest.Cuota, ori => ori.MapFrom(src => src.Cuota.GetClassName()));
            CreateMap<CuotaPorSocioRequestDto, CuotaPorSocio>();
        }
    }
}
