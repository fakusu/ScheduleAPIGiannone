using Application.Dtos.Tarea;
using AutoMapper;
using Entities;
using System.Runtime.InteropServices;

namespace WebApi.Mapping
{
    public class TareaMappingProfile:Profile
    {
        public TareaMappingProfile()
        {
            CreateMap<Tarea,TareaResponseDto>();
            CreateMap<TareaRequestDto,Tarea>();
        }
    }
}
