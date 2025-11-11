using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Admin.Cuota
{
    public class CuotaRequestDto
    {
        public decimal Valor { get; set; }
        [DataType(DataType.Date)]
        public DateTime CaducaEn { get; set; }
        public int IdAnio { get; set; }
    }
}
