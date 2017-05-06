using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class HomeTestViewModel
    {
        // old type resources - strongly typed
        [MaxLength(10, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "FieldMaxLength")]
        [MinLength(5, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "FieldMinLength")]
        [Display(ResourceType = typeof(Resources.Misc), Name = "PersonLabel")]
        // asp.net core resources
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
    }
}
