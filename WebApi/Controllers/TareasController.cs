using Application;
using Application.Dtos.Tarea;

using AutoMapper;
using Entities;
using Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Numerics;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TareasController> _logger;
        private readonly IApplication<Tarea> _tarea;
        private readonly IMapper _mapper;
        public TareasController(ILogger<TareasController> logger, IApplication<Tarea> tarea, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _tarea = tarea;
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
                    return Ok(_mapper.Map<IList<TareaResponseDto>>(_tarea.GetAll()));
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
                    var tarea = _tarea.GetById(Id.Value);

                    if (tarea is null)
                        return NotFound("Tarea no encontrada.");

                    return Ok(_mapper.Map<TareaResponseDto>(tarea));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la tarea por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(TareaRequestDto tareaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var tarea = _mapper.Map<Tarea>(tareaRequestDto);
                _tarea.Save(tarea);
                return Ok(tarea.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la tarea.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, TareaRequestDto tareaRequestDto)
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
                Tarea tareaBack = _tarea.GetById(Id.Value);
                if (tareaBack is null)
                {
                    return NotFound();
                }
                tareaBack = _mapper.Map<Tarea>(tareaRequestDto);
                _tarea.Save(tareaBack);
                return Ok(tareaBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la tarea.");
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
                Tarea tareaBack = _tarea.GetById(Id.Value);
                if (tareaBack is null)
                {
                    return NotFound();
                }
                _tarea.Delete(tareaBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar la tarea.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
