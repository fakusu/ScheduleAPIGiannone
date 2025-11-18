using Application;
using Application.Dtos.Recompensa;
using AutoMapper;
using Entities;
using Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RecompensasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RecompensasController> _logger;
        private readonly IApplication<Recompensa> _recompensa;
        private readonly IMapper _mapper;
        public RecompensasController(ILogger<RecompensasController> logger, IApplication<Recompensa> recompensa, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _recompensa = recompensa;
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
                    return Ok(_mapper.Map<IList<RecompensaResponseDto>>(_recompensa.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las recompensas.");
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
                    var recompensa = _recompensa.GetById(Id.Value);

                    if (recompensa is null)
                        return NotFound("Recompensa no encontrada.");

                    return Ok(_mapper.Map<RecompensaResponseDto>(recompensa));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la recompensa por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(RecompensaRequestDto recompensaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var recompensa = _mapper.Map<Recompensa>(recompensaRequestDto);
                _recompensa.Save(recompensa);
                return Ok(recompensa.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la recompensa.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, RecompensaRequestDto recompensaRequestDto)
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
                Recompensa recompensaBack = _recompensa.GetById(Id.Value);
                if (recompensaBack is null)
                {
                    return NotFound();
                }
                recompensaBack = _mapper.Map<Recompensa>(recompensaRequestDto);
                _recompensa.Save(recompensaBack);
                return Ok(recompensaBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la recompensa.");
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
                Recompensa recompensaBack = _recompensa.GetById(Id.Value);
                if (recompensaBack is null)
                {
                    return NotFound();
                }
                _recompensa.Delete(recompensaBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar la recompensa.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}

