using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Entities
{
    public sealed class AuthLoginResult
    {
        public string AccessToken { get; init; } = default!;
        public string? IdToken { get; init; }
        public string TokenType { get; init; } = default!;
        public int ExpiresIn { get; init; }
    }
}
