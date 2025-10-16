using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.EstadisticaUsuario
{
    public class EstadisticaUsuarioResponseDto
    {
        public int Id { get; set; }


        public int IdUsuario { get; set; }
        public int TotalTareasCompletadas { get; set; }
        public int TotalHabitosBuenos { get; set; }
        public int TotalHabitosMalos { get; set; }
        public DateTime UltimaConexion { get; set; }
        public int Nivel { get; set; }
        public int ExperienciaTotal { get; set; }
    }
}
