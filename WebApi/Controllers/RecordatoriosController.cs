using Application;
using Application.Dtos.Recordatorio;
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
    public class RecordatoriosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RecordatoriosController> _logger;
        private readonly IApplication<Recordatorio> _recordatorio;
        private readonly IMapper _mapper;
        public RecordatoriosController(ILogger<RecordatoriosController> logger, IApplication<Recordatorio> recordatorio, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _recordatorio = recordatorio;
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
                    return Ok(_mapper.Map<IList<RecordatorioResponseDto>>(_recordatorio.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los recordatorios.");
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
                    var recordatorio = _recordatorio.GetById(Id.Value);

                    if (recordatorio is null)
                        return NotFound("Recordatorio no encontrado.");

                    return Ok(_mapper.Map<RecordatorioResponseDto>(recordatorio));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el recordatorio por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(RecordatorioRequestDto recordatorioRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var recordatorio = _mapper.Map<Recordatorio>(recordatorioRequestDto);
                _recordatorio.Save(recordatorio);
                return Ok(recordatorio.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el recordatorio.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, RecordatorioRequestDto recordatorioRequestDto)
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
                Recordatorio recordatorioBack = _recordatorio.GetById(Id.Value);
                if (recordatorioBack is null)
                {
                    return NotFound();
                }
                recordatorioBack = _mapper.Map<Recordatorio>(recordatorioRequestDto);
                _recordatorio.Save(recordatorioBack);
                return Ok(recordatorioBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el recordatorio.");
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
                Recordatorio recordatorioBack = _recordatorio.GetById(Id.Value);
                if (recordatorioBack is null)
                {
                    return NotFound();
                }
                _recordatorio.Delete(recordatorioBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el recordatorio.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
