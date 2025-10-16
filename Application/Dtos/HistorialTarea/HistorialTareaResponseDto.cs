using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.HistorialTarea
{
    public class HistorialTareaResponseDto
    {   
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public DateTime FechaCompletada { get; set; }
        public int ValorGanadoXP { get; set; }
    }
}
