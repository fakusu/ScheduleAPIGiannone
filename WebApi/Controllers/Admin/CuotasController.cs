using Application;
using Application.Dtos.Admin.Cuota;
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
    public class CuotasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CuotasController> _logger;
        private readonly IApplication<Cuota> _cuota;
        private readonly IApplication<Mes> _mes;
        private readonly IMapper _mapper;
        public CuotasController(
            ILogger<CuotasController> logger
            , UserManager<User> userManager
            , IApplication<Cuota> cuota
            , IMapper mapper
            , IApplication<Mes> mes)
        {
            _logger = logger;
            _cuota = cuota;
            _mapper = mapper;
            _userManager = userManager;
            _mes = mes;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(user, "Administrador").Result)
            {
                return Ok(_mapper.Map<IList<CuotaResponseDto>>(_cuota.GetAll()));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("CuotasPorAnio")]
        public async Task<IActionResult> Crear(CuotaRequestDto cuotaRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            foreach (var mes in _mes.GetAll())
            {
                Cuota cuota = new Cuota
                {
                    Id = 0,
                    IdMes = mes.Id,
                    IdAnio = cuotaRequestDto.IdAnio
                };
                cuota.SetCaducidad(cuotaRequestDto.CaducaEn);
                cuota.SetValor(cuotaRequestDto.Valor);
                _cuota.Save(cuota);
            }
            return Ok("Cuotas creadas");
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, CuotaRequestDto cuotaRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var cuota = _cuota.GetById(Id.Value);
            if (cuota is null)
            { return NotFound(); }
            cuota.SetCaducidad(cuotaRequestDto.CaducaEn);
            cuota.SetValor(cuotaRequestDto.Valor);
            _cuota.Save(cuota);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Cuota anioBack = _cuota.GetById(Id.Value);
            if (anioBack is null)
            { return NotFound(); }
            _cuota.Delete(anioBack.Id);
            return Ok();
        }
    }
}
