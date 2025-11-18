using Application.Dtos.Recordatorio;
using Application.Dtos.Tarea;
using AutoMapper;
using Entities;

namespace WebApi.Mapping
{
    public class RecordatorioMappingProfile:Profile
    {
        public RecordatorioMappingProfile()
        {
            CreateMap<Recordatorio, RecordatorioResponseDto>();
            CreateMap<RecordatorioRequestDto, Recordatorio>();
        }
    }
}
