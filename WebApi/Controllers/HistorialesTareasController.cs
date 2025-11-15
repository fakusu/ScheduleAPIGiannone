using Application;
using Application.Dtos.HistorialTarea;
using Application.Dtos.HistorialTarea;
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
    public class HistorialesTareasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HistorialesTareasController> _logger;
        private readonly IApplication<HistorialTarea> _historialTarea;
        private readonly IMapper _mapper;
        public HistorialesTareasController(ILogger<HistorialesTareasController> logger, IApplication<HistorialTarea> historialTarea, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _historialTarea = historialTarea;
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
                    return Ok(_mapper.Map<IList<HistorialTareaResponseDto>>(_historialTarea.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los historiales.");
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
                    var historialTarea = _historialTarea.GetById(Id.Value);

                    if (historialTarea is null)
                        return NotFound("HistorialTarea no encontrada.");

                    return Ok(_mapper.Map<HistorialTareaResponseDto>(historialTarea));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(HistorialTareaRequestDto historialTareaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var historialTarea = _mapper.Map<HistorialTarea>(historialTareaRequestDto);
                _historialTarea.Save(historialTarea);
                return Ok(historialTarea.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, HistorialTareaRequestDto historialTareaRequestDto)
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
                HistorialTarea historialTareaBack = _historialTarea.GetById(Id.Value);
                if (historialTareaBack is null)
                {
                    return NotFound();
                }
                historialTareaBack = _mapper.Map<HistorialTarea>(historialTareaRequestDto);
                _historialTarea.Save(historialTareaBack);
                return Ok(historialTareaBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el historial de tarea.");
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
                HistorialTarea historialTareaBack = _historialTarea.GetById(Id.Value);
                if (historialTareaBack is null)
                {
                    return NotFound();
                }
                _historialTarea.Delete(historialTareaBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el historial de Tarea.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
