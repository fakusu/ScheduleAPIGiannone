using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Login
{
    public class LoginUserResponseDto
    {
        public string Token { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool Login { get; set; }
        public List<string> Errores { get; set; }


    }
}
