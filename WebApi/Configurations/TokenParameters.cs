using Abstractions;

namespace WebApi.Configurations
{
    public class TokenParameters : ITokensParameters
    {
        public string UserName { get; set ; }
        public string Email { get; set ; }
        public string PasswordHash { get ; set ; }
        public string Id { get ; set ; }
    }
}
