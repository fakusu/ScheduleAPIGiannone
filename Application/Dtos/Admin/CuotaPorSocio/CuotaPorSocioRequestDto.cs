using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Admin.CuotaPorSocio
{
    public class CuotaPorSocioRequestDto
    {
        public int Id { get; set; }
        public int IdSocio { get; set; }
        public int IdCuota { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }
        
    }
}
