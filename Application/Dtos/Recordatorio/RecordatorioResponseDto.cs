using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Recordatorio
{
    public class RecordatorioResponseDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Mensaje { get; set; }
        [StringLength(30)]
        [ForeignKey(nameof(Tarea))]
        public int TareaId { get; set; }
    }
}
