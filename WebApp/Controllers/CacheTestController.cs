using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CacheTestController : Controller
    {
        [ResponseCache(VaryByQueryKeys = new []{"foo","bar"}, Duration = 30)]
        public IActionResult Index(string foo, string bar)
        {
            return View();
        }
    }
}