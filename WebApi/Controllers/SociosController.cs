using Application;
using Application.Dtos.Socio;
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
    public class SociosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SociosController> _logger;
        private readonly IApplication<Socio> _socio;
        private readonly IMapper _mapper;
        public SociosController(
            ILogger<SociosController> logger
            , UserManager<User> userManager
            , IApplication<Socio> socio
            , IMapper mapper)
        {
            _logger = logger;
            _socio = socio;
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
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<SocioResponseDto>>(_socio.GetAll()));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Socio socio = _socio.GetById(Id.Value);
            if (socio is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SocioResponseDto>(socio));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SocioRequestDto socioRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var socio = _mapper.Map<Socio>(socioRequestDto);
            _socio.Save(socio);
            return Ok(socio.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, SocioRequestDto socioRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Socio socioBack = _socio.GetById(Id.Value);
            if (socioBack is null)
            { return NotFound(); }
            socioBack = _mapper.Map<Socio>(socioRequestDto);
            _socio.Save(socioBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Socio socioBack = _socio.GetById(Id.Value);
            if (socioBack is null)
            { return NotFound(); }
            _socio.Delete(socioBack.Id);
            return Ok();
        }
    }
}
