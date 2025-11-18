using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MetaUsuario
{
    public class MetaUsuarioRequestDto
    {
        public int Id { get; set; }

       
        public Guid UsuarioId { get; set; }

      
        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int CantidadObjetivo { get; set; }
        public int CantidadActual { get; set; }

        public bool Completada => CantidadActual >= CantidadObjetivo;
    }
}
