using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MetaUsuario
{
    public class MetaUsuarioResponseDto
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
