using Application;
using Application.Dtos.Usuario;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IApplication<Usuario> _usuario;
        public readonly IMapper _mapper;
        public UsuariosController(ILogger<UsuariosController> logger, IApplication<Usuario> usuario, IMapper mapper)
        {
            _logger = logger;
            _usuario = usuario;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<UsuarioResponseDto>>(_usuario.GetAll())   );
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Usuario usuario = _usuario.GetById(Id.Value);
            if (usuario is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UsuarioResponseDto>(usuario));
        }
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioRequestDto usuarioRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var usuario = _mapper.Map<Usuario>(usuarioRequestDto);
            _usuario.Save(usuario);
            return Ok(usuario.Id);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, UsuarioRequestDto usuarioRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Usuario usuarioBack = _usuario.GetById(Id.Value);
            if (usuarioBack is null)
            {
                return NotFound();
            }
            usuarioBack = _mapper.Map<Usuario>(usuarioRequestDto);
            _usuario.Save(usuarioBack);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Usuario usuarioBack = _usuario.GetById(Id.Value);
            if (usuarioBack == null)
            {
                return NotFound();
            }
            _usuario.Delete(usuarioBack.Id);
            return Ok();
        }
    }
}
