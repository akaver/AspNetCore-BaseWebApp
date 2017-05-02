using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Identity.ViewModels
{
    public class IdentityUserClaimsCreateEditViewModel
    {
        public IdentityUserClaim IdentityUserClaim { get; set; }

        public SelectList UserSelectList { get; set; }
    }

}
