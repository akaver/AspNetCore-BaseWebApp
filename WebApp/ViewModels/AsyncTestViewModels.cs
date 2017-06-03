using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class AsyncTestIndexViewModel
    {
        public DateTime RequestStartAt { get; set; }
        public List<string> Res { get; set; } = new List<string>();
        public DateTime RequestDoneAt { get; set; }

    }
}
