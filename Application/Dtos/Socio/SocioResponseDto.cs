using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Socio
{
    public class SocioResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaIngreso { get; set; }
        public string? FechaBaja { get; set; }
        public string TelefonoMovil { get; set; }
        public string? TelefonoFijo { get; set; }
    }
}
