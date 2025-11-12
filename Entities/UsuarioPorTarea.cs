using Abstractions;
using Entities.MicrosoftIdentity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UsuarioPorTarea : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid IdUsuario { get; set; }
        [ForeignKey(nameof(Tarea))]
        public int IdTarea { get; set; }
        public virtual User User { get; set; }
        public virtual Tarea Tarea { get; set; }
    }
}
