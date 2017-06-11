using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.RestServer.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/IdentityUser")]
    public class IdentityUserController : Controller
    {
        private readonly IApplicationUnitOfWork _uow;

        public IdentityUserController(IApplicationUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet(template: "FindByEmailAsync/{normalizedEmail}", Name = "IdentityUser_FindByEmailAsync")]
        public async Task<IActionResult> FindByEmailAsync(string normalizedEmail)
        {
            var res = await _uow.IdentityUsers.FindByEmailAsync(normalizedEmail: normalizedEmail);
            return new ObjectResult(value: res);
        }

    }
}
