using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class TipoTarea:IEntidad
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; }

        public virtual List<Tarea> Tareas { get; set; }
    }
}
