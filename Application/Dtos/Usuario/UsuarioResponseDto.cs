using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Usuario
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
  
        public string Nombre { get; set; }
     
        public string Apellido { get; set; }

        public string Email { get; set; }

        public int Nivel { get; set; }

        public int Experiencia { get; set; }
    }
}
