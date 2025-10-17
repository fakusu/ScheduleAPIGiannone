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
    public class Tarea:IEntidad
    {
        public Tarea()
        {
                UsuariosPorTareas = new HashSet<UsuarioPorTarea>();
                Historial = new List<HistorialTarea>();
        }
        public int Id { get; set; }
        [StringLength(30)]
        public string Titulo { get; set; }
        [StringLength(30)]
        public string  Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }
        [ForeignKey(nameof(TipoTarea))]
        public int TipoTareaId { get; set; }

        public int Valor { get; set; }

        public bool Completada { get; set; }

        public virtual ICollection<UsuarioPorTarea> UsuariosPorTareas { get; set; }
        public virtual List<HistorialTarea> Historial { get; set; }
        public virtual TipoTarea TipoTarea { get; set; }
    }
}
