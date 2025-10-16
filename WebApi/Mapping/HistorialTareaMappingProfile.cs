using AutoMapper;
using Entities;
using Application.Dtos.HistorialTarea;

namespace WebApi.Mapping
{
    public class HistorialTareaMappingProfile:Profile
    {
        public HistorialTareaMappingProfile()
        {
            CreateMap<HistorialTarea, HistorialTareaResponseDto>();
            CreateMap<HistorialTareaRequestDto, HistorialTarea>();
        }
    }
}
