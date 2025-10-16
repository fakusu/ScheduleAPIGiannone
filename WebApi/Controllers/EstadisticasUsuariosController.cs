using Application;
using Application.Dtos.EstadisticaUsuario;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasUsuariosController : ControllerBase
    {
        private readonly ILogger<EstadisticasUsuariosController> _logger;
        private readonly IApplication<EstadisticaUsuario> _estadisticaUsuario;
        public readonly IMapper _mapper;
        public EstadisticasUsuariosController(ILogger<EstadisticasUsuariosController> logger, IApplication<EstadisticaUsuario> estadisticaUsuario, IMapper mapper)
        {
            _logger = logger;
            _estadisticaUsuario = estadisticaUsuario;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<EstadisticaUsuarioResponseDto>>(_estadisticaUsuario.GetAll()));
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            EstadisticaUsuario estadisticaUsuario = _estadisticaUsuario.GetById(Id.Value);
            if (estadisticaUsuario is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EstadisticaUsuarioResponseDto>(estadisticaUsuario));
        }
        [HttpPost]
        public async Task<IActionResult> Crear(EstadisticaUsuarioRequestDto estadisticaUsuarioRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var estadisticaUsuario = _mapper.Map<EstadisticaUsuario>(estadisticaUsuarioRequestDto);
            _estadisticaUsuario.Save(estadisticaUsuario);
            return Ok(estadisticaUsuario.Id);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, EstadisticaUsuarioRequestDto estadisticaUsuarioRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            EstadisticaUsuario estadisticaUsuarioBack = _estadisticaUsuario.GetById(Id.Value);
            if (estadisticaUsuarioBack is null)
            {
                return NotFound();
            }
            estadisticaUsuarioBack = _mapper.Map<EstadisticaUsuario>(estadisticaUsuarioRequestDto);
            _estadisticaUsuario.Save(estadisticaUsuarioBack);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            EstadisticaUsuario estadisticaUsuarioBack = _estadisticaUsuario.GetById(Id.Value);
            if (estadisticaUsuarioBack == null)
            {
                return NotFound();
            }
            _estadisticaUsuario.Delete(estadisticaUsuarioBack.Id);
            return Ok();
        }
    }
}
