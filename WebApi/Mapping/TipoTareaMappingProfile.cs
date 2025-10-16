using Application.Dtos.TipoTarea;
using AutoMapper;
using Entities;

namespace WebApi.Mapping
{
    public class TipoTareaMappingProfile:Profile
    {
        public TipoTareaMappingProfile()
        {
            CreateMap<TipoTarea, TipoTareaResponseDto>();
            CreateMap<TipoTareaRequestDto, TipoTarea>();
        }
    }
}
