using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Admin.CuotaPorSocio
{
    public class CuotaPorSocioResponseDto
    {
        public int Id { get; set; }
        public string Socio { get; set; }
        public string Cuota { get; set; }
        public decimal Valor { get; set; }
        public decimal Recargo { get; set; }
        public decimal Total { get; set; }
        public string? FechaPago { get; set; }
    }
}
