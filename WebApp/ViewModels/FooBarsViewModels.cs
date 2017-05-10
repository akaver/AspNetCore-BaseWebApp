using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class FooBarsCreateEditViewModel
    {
        public FooBar FooBar { get; set; }

        public SelectList BlahOneSelectList { get; set; }
        public SelectList BlahTwoSelectList { get; set; }
        public SelectList BlahThreeSelectList { get; set; }

    }


}
