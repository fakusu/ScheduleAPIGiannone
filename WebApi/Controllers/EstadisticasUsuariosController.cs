using Application;
using Application.Dtos.EstadisticaUsuario;
using AutoMapper;
using Entities;
using Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasUsuariosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EstadisticasUsuariosController> _logger;
        private readonly IApplication<EstadisticaUsuario> _estadisticaUsuario;
        private readonly IMapper _mapper;
        public EstadisticasUsuariosController(ILogger<EstadisticasUsuariosController> logger, IApplication<EstadisticaUsuario> estadisticaUsuario, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _estadisticaUsuario = estadisticaUsuario;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> All()
        {
            try
            {
                var id = User.FindFirst("Id").Value.ToString();
                var user = _userManager.FindByIdAsync(id).Result;
                if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                    await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var name = User.FindFirst("name");
                    var a = User.Claims;
                    return Ok(_mapper.Map<IList<EstadisticaUsuarioResponseDto>>(_estadisticaUsuario.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las estadisticas.");
                return StatusCode(500, "Ocurrió un error al proceprocesar la solicitud.");
            }
        }



        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ById(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                    return BadRequest("Debe especificar un Id.");
                var idUser = User.FindFirst("Id")?.Value;
                var user = await _userManager.FindByIdAsync(idUser);

                if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                    await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var estadisticaUsuario = _estadisticaUsuario.GetById(Id.Value);

                    if (estadisticaUsuario is null)
                        return NotFound("Estadisticas no encontrada.");

                    return Ok(_mapper.Map<EstadisticaUsuarioResponseDto>(estadisticaUsuario));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las estadisticas por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(EstadisticaUsuarioRequestDto tareaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var estadisticaUsuario = _mapper.Map<EstadisticaUsuario>(tareaRequestDto);
                _estadisticaUsuario.Save(estadisticaUsuario);
                return Ok(estadisticaUsuario.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear las estadisticas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, EstadisticaUsuarioRequestDto tareaRequestDto)
        {
            try
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
                estadisticaUsuarioBack = _mapper.Map<EstadisticaUsuario>(tareaRequestDto);
                _estadisticaUsuario.Save(estadisticaUsuarioBack);
                return Ok(estadisticaUsuarioBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar las estadisticas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid) return BadRequest();
                EstadisticaUsuario estadisticaUsuarioBack = _estadisticaUsuario.GetById(Id.Value);
                if (estadisticaUsuarioBack is null)
                {
                    return NotFound();
                }
                _estadisticaUsuario.Delete(estadisticaUsuarioBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar las estadisticas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
