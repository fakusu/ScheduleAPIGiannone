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
    public class EstadisticaUsuario:IEntidad
    {
        
        public int Id { get; set; }

             
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }
        public int TotalTareasCompletadas { get; set; }
        public int TotalHabitosBuenos { get; set; }       
        public int TotalHabitosMalos { get; set; }
        public DateTime UltimaConexion { get; set; }
        
    }
}
