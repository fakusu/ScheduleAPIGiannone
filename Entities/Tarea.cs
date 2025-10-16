using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tarea
    {
        public Tarea()
        {
                UsuariosPorTareas = new HashSet<UsuarioPorTarea>();
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
        
        public int Valor { get; set; }

        public bool Completada { get; set; }

        public virtual ICollection<UsuarioPorTarea> UsuariosPorTareas { get; set; }
        public virtual List<HistorialTarea> Historial { get; set; }
        public virtual TipoTarea TipoTarea { get; set; }
    }
}
