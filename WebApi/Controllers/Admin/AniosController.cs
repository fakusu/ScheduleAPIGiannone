using Application;
using Application.Dtos.Admin.Anio;
using AutoMapper;
using Entities;
using Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AniosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AniosController> _logger;
        private readonly IApplication<Anio> _anio;
        private readonly IMapper _mapper;
        public AniosController(
            ILogger<AniosController> logger
            , UserManager<User> userManager
            , IApplication<Anio> anio
            , IMapper mapper)
        {
            _logger = logger;
            _anio = anio;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(user, "Administrador").Result)
            {
                return Ok(_mapper.Map<IList<AnioResponseDto>>(_anio.GetAll()));
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(AnioRequestDto anioRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var anio = _mapper.Map<Anio>(anioRequestDto);
            _anio.Save(anio);
            return Ok(anio.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Anio anioBack = _anio.GetById(Id.Value);
            if (anioBack is null)
            { return NotFound(); }
            _anio.Delete(anioBack.Id);
            return Ok();
        }
    }
}
