using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Socio
{
    public class SocioRequestDto
    {
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(30)]
        public string Apellido { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaBaja { get; set; }
        public string TelefonoMovil { get; set; }
        public string? TelefonoFijo { get; set; }
    }
}
