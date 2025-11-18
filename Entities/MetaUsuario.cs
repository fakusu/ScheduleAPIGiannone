using Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MetaUsuario:IEntidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public IdentityUser<Guid> Usuario { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int CantidadObjetivo { get; set; }
        public int CantidadActual { get; set; }

        public bool Completada => CantidadActual >= CantidadObjetivo;
    }

}
