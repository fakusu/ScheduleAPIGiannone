using Application;
using Application.Dtos.MetaUsuario;
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
    public class MetasUsuariosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<MetasUsuariosController> _logger;
        private readonly IApplication<MetaUsuario> _metaUsuario;
        private readonly IMapper _mapper;
        public MetasUsuariosController(ILogger<MetasUsuariosController> logger, IApplication<MetaUsuario> metaUsuario, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _metaUsuario = metaUsuario;
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
                    return Ok(_mapper.Map<IList<MetaUsuarioResponseDto>>(_metaUsuario.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las metas.");
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
                    var metaUsuario = _metaUsuario.GetById(Id.Value);

                    if (metaUsuario is null)
                        return NotFound("Metas no encontrada.");

                    return Ok(_mapper.Map<MetaUsuarioResponseDto>(metaUsuario));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las metas por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Crear(MetaUsuarioRequestDto tareaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var metaUsuario = _mapper.Map<MetaUsuario>(tareaRequestDto);
                _metaUsuario.Save(metaUsuario);
                return Ok(metaUsuario.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear las metas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Editar(int? Id, MetaUsuarioRequestDto tareaRequestDto)
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
                MetaUsuario metaUsuarioBack = _metaUsuario.GetById(Id.Value);
                if (metaUsuarioBack is null)
                {
                    return NotFound();
                }
                metaUsuarioBack = _mapper.Map<MetaUsuario>(tareaRequestDto);
                _metaUsuario.Save(metaUsuarioBack);
                return Ok(metaUsuarioBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar las metas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid) return BadRequest();
                MetaUsuario metaUsuarioBack = _metaUsuario.GetById(Id.Value);
                if (metaUsuarioBack is null)
                {
                    return NotFound();
                }
                _metaUsuario.Delete(metaUsuarioBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar las metas.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
