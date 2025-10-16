using Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Usuario:IEntidad
    {
        public Usuario()
        {
            UsuariosPorTareas = new HashSet<UsuarioPorTarea>();
        }
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(30)]
        public string Apellido { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public int Nivel { get; set; }
      
        public int Experiencia { get; set; }
        


        public virtual ICollection<UsuarioPorTarea> UsuariosPorTareas { get; set; }
        

    }
}
