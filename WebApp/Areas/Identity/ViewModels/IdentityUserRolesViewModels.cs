using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Identity.ViewModels
{
    public class IdentityUserRolesCreateEditViewModel
    {
        public IdentityUserRole IdentityUserRole { get; set; }
        public SelectList UserSelectList { get; set; }
        public SelectList RoleSelectList { get; set; }
    }
}
