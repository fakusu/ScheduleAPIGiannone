using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recordatorio:IEntidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TareaId { get; set; }
        [ForeignKey("TareaId")]
        public virtual Tarea Tarea { get; set; }

        [Required]
        public DateTime FechaRecordatorio { get; set; }

        [MaxLength(250)]
        public string Mensaje { get; set; }
    }

}
