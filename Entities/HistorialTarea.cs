using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HistorialTarea:IEntidad
    {
        public int Id { get; set; }
        public int IdTarea { get; set; }
        [ForeignKey(nameof(IdTarea))]
        public virtual Tarea Tarea { get; set; }
        public DateTime FechaCompletada { get; set; } = DateTime.Now;
        public int ValorGanadoXP { get; set; }=0;


       
    }
}
