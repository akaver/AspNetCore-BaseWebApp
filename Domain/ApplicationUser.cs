using System;
using AspNetCore.Identity.Uow.Models;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        // custom fields go here
        public string FooBar { get; set; }
    }
}
