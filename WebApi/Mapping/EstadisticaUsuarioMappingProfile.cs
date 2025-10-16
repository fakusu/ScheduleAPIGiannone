namespace WebApi.Mapping;
    using AutoMapper;
    using Application.Dtos.EstadisticaUsuario;
    using Entities;

public class EstadisticaUsuarioMappingProfile:Profile   
    {
    public EstadisticaUsuarioMappingProfile()
    {
       CreateMap<EstadisticaUsuario, EstadisticaUsuarioResponseDto>();
        CreateMap<EstadisticaUsuarioRequestDto, EstadisticaUsuario>();
    }
}

