using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;

        public HomeController(IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }


        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                key: CookieRequestCultureProvider.DefaultCookieName,
                value: CookieRequestCultureProvider.MakeCookieValue(
                    requestCulture: new RequestCulture(culture: culture)),
                options: new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(years: 1)
                }
            );

            return LocalRedirect(localUrl: returnUrl);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData[index: "Message"] = 
                _localizer[name: "Nice app! no-translation."] +  " " 
                + _sharedLocalizer["Shared info! no-translation!"];

            return View();
        }

        public IActionResult Contact()
        {
            ViewData[index: "Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Test()
        {
            var vm = new HomeTestViewModel();
            vm.FirstName = "Andres";
            return View(vm);
        }

        [HttpPost]
        public IActionResult Test(HomeTestViewModel vm)
        {
            // just this is not working, data is not updated for html helpers or tag helpers
            vm.FirstName = "Mait";
            vm.DecimalNumber = vm.DecimalNumber + 1;
            vm.DoubleNumber = vm.DoubleNumber + 1;

            // you need to update the modelstate for fields that are presented in post!!!!!!!
            // read this: https://stackoverflow.com/questions/2686652/how-to-modify-posted-form-data-within-controller-action-before-sending-to-view 
            // http://garyclarke.us/programming/consumption-of-data-in-mvc2-views/
            ModelState.FirstOrDefault(x => x.Key == nameof(vm.FirstName)).Value.RawValue = vm.FirstName;
            ModelState.FirstOrDefault(x => x.Key == nameof(vm.DecimalNumber)).Value.RawValue = vm.DecimalNumber;
            ModelState.FirstOrDefault(x => x.Key == nameof(vm.DoubleNumber)).Value.RawValue = vm.DoubleNumber;

            return View(vm);
        }


    }
}
