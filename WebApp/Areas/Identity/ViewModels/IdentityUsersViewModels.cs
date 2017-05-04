using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Identity.ViewModels
{
    public class IdentityUsersManageRolesViewModel
    {
        public IdentityUser IdentityUser { get; set; }

        public IEnumerable<IdentityRole> AllRoles { get; set; }

        public List<int> SelectedRoleIds { get; set; }

    }

}
