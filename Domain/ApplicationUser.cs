using System;
using System.Collections.Generic;
using AspNetCore.Identity.Uow.Models;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        // custom fields go here
        public string NickName { get; set; }

        public List<FooBar> FooBars { get; set; }
    }
}
