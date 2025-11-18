using Application.Dtos.Recompensa;

using Entities;
using AutoMapper;

namespace WebApi.Mapping
{
    public class RecompensaMappingProfile:Profile
    {
        public RecompensaMappingProfile()
        {
            CreateMap<Recompensa, RecompensaResponseDto>();
            CreateMap<RecompensaRequestDto, Recompensa>();
        }
    }
}
