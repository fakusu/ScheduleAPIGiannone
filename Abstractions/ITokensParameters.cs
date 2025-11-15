using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface ITokensParameters
    {
        string UserName { get; set; }  
        string Email { get; set; }  
        string PasswordHash { get; set; }
        string Id { get; set; }
        IList<string>? Roles { get; set; }
    }
}
