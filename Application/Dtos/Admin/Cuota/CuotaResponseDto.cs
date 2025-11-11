using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Admin.Cuota
{
    public class CuotaResponseDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string CaducaEn { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Informacion { get; set; }
    }
}
