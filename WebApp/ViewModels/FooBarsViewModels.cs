using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class FooBarsCreateEditViewModel
    {
        public FooBar FooBar { get; set; }

        [MaxLength(20)]
        public string Wibble { get; set; }
        [MaxLength(10)]
        public string Wobble { get; set; }

        public SelectList BlahOneSelectList { get; set; }
        public SelectList BlahTwoSelectList { get; set; }
        public SelectList BlahThreeSelectList { get; set; }

    }


}
