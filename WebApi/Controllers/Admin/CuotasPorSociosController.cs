using Application;
using Application.Dtos.Admin.CuotaPorSocio;
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
    public class CuotasPorSociosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CuotasPorSociosController> _logger;
        private readonly IApplication<CuotaPorSocio> _coutaPorSocio;
        private readonly IApplication<Cuota> _couta;
        private readonly IMapper _mapper;
        public CuotasPorSociosController(
            ILogger<CuotasPorSociosController> logger
            , UserManager<User> userManager
            , IApplication<CuotaPorSocio> coutaPorSocio
            , IMapper mapper
            , IApplication<Cuota> couta)
        {
            _logger = logger;
            _coutaPorSocio = coutaPorSocio;
            _mapper = mapper;
            _userManager = userManager;
            _couta = couta;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All(int idSocio)
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            try
            {
                if (_userManager.IsInRoleAsync(user, "Administrador").Result)
                {
                    return Ok(_mapper.Map<IList<CuotaPorSocioResponseDto>>(_coutaPorSocio.GetAll().Where(c => c.IdSocio == idSocio).ToList()));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CuotaPorSocioRequestDto cuotaPorSocioRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var cuotaPorSocio = _mapper.Map<CuotaPorSocio>(cuotaPorSocioRequestDto);
            cuotaPorSocio.Cuota = _couta.GetById(cuotaPorSocio.IdCouta);
            cuotaPorSocio.SetValor(cuotaPorSocio.Cuota);
            cuotaPorSocio.SetRecargo(cuotaPorSocio.Cuota);
            cuotaPorSocio.SetTotal();
            _coutaPorSocio.Save(cuotaPorSocio);
            return Ok(cuotaPorSocio.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            CuotaPorSocio coutaPorSocioBack = _coutaPorSocio.GetById(Id.Value);
            if (coutaPorSocioBack is null)
            { return NotFound(); }
            _coutaPorSocio.Delete(coutaPorSocioBack.Id);
            return Ok();
        }
    }
}
