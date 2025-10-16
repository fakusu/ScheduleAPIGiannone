using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Tarea
{
    public class TareaRequestDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Titulo { get; set; }
        [StringLength(30)]
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        public int Valor { get; set; }

        public bool Completada { get; set; }
    }
}
