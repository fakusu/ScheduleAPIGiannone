using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Tarea
{
    public class TareaResponseDto
    {
        public int Id { get; set; }
       
        public string Titulo { get; set; }
       
        public string Descripcion { get; set; }
       
        public DateTime FechaCreacion { get; set; }
       
        public DateTime FechaVencimiento { get; set; }

        public int Valor { get; set; }

        public bool Completada { get; set; }
    }
}
