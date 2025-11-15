using Application;
using Application.Dtos.Tarea;
using Application.Dtos.TipoTarea;
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
    public class TiposTareasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TiposTareasController> _logger;
        private readonly IApplication<TipoTarea> _tipoTarea;
        private readonly IMapper _mapper;
        public TiposTareasController(ILogger<TiposTareasController> logger, IApplication<TipoTarea> tipoTarea, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _tipoTarea = tipoTarea;
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
                    return Ok(_mapper.Map<IList<TipoTareaResponseDto>>(_tipoTarea.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las tareas.");
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
                    var tipoTarea = _tipoTarea.GetById(Id.Value);

                    if (tipoTarea is null)
                        return NotFound("TipoTarea no encontrada.");

                    return Ok(_mapper.Map<TipoTareaResponseDto>(tipoTarea));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el tipo de tarea por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(TipoTareaRequestDto tipoTareaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var tipoTarea = _mapper.Map<TipoTarea>(tipoTareaRequestDto);
                _tipoTarea.Save(tipoTarea);
                return Ok(tipoTarea.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo tarea.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, TipoTareaRequestDto tipoTareaRequestDto)
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
                TipoTarea tipoTareaBack = _tipoTarea.GetById(Id.Value);
                if (tipoTareaBack is null)
                {
                    return NotFound();
                }
                tipoTareaBack = _mapper.Map<TipoTarea>(tipoTareaRequestDto);
                _tipoTarea.Save(tipoTareaBack);
                return Ok(tipoTareaBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el tipo tarea.");
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
                TipoTarea tipoTareaBack = _tipoTarea.GetById(Id.Value);
                if (tipoTareaBack is null)
                {
                    return NotFound();
                }
                _tipoTarea.Delete(tipoTareaBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el tipo de tarea.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}

