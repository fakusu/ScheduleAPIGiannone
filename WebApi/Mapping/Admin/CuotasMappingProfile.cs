using Application.Dtos.Admin.Cuota;
using AutoMapper;
using Entities;

namespace WebApi.Mapping.Admin
{
    public class CuotasMappingProfile : Profile
    {
        public CuotasMappingProfile()
        {
            CreateMap<Cuota, CuotaResponseDto>().
                ForMember(dest => dest.CaducaEn, ori => ori.MapFrom(src => src.CaducaEn.ToShortDateString())).
                ForMember(dest => dest.Mes, ori => ori.MapFrom(src => src.Mes.Nombre)).
                ForMember(dest => dest.Anio, ori => ori.MapFrom(src => src.Anio.Numero.ToString())).
                ForMember(dest => dest.Informacion, ori => ori.MapFrom(src => src.GetClassName()));
            CreateMap<CuotaRequestDto, Cuota>();
        }
    }
}
