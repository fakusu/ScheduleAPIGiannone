using Application;
using Application.Dtos.Admin.Mes;
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
    public class MesesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<MesesController> _logger;
        private readonly IApplication<Mes> _mes;
        private readonly IMapper _mapper;
        public MesesController(
            ILogger<MesesController> logger
            , UserManager<User> userManager
            , IApplication<Mes> mes
            , IMapper mapper)
        {
            _logger = logger;
            _mes = mes;
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
                return Ok(_mapper.Map<IList<MesResponseDto>>(_mes.GetAll()));
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(MesRequestDto mesRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var mes = _mapper.Map<Mes>(mesRequestDto);
            _mes.Save(mes);
            return Ok(mes.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Mes mesBack = _mes.GetById(Id.Value);
            if (mesBack is null)
            { return NotFound(); }
            _mes.Delete(mesBack.Id);
            return Ok();
        }
    }
}
