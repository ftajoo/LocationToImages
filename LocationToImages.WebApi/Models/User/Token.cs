using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationToImages.WebApi.Models.User
{
    public class Token
    {
        public User User { get; set; }

        public string JwtToken { get; set; }

        public int Expires { get; set; }
    }
}