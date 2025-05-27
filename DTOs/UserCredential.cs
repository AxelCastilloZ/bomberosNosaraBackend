using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserCredential(string Email, string Password);
    public record TokenResponse(string Token);
}
