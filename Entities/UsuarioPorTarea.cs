using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuarioPorTarea : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(Tarea))]
        public int IdTarea { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Tarea Tarea { get; set; }
    }
}
