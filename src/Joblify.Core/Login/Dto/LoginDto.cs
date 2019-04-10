using System;
using System.Collections.Generic;
using System.Text;

namespace Joblify.Core.Login.Dto
{
    public class LoginDto
    {
        public string ExternalProviderToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
    }
}
