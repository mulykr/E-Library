using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiBook.Data.Entities
{
    public class UserProfile:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistredOn { get; set; }
    }
}
