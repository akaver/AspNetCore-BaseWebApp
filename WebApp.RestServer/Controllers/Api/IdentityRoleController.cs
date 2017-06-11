using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.RestServer.Controllers.Api
{
    [Produces(contentType: "application/json")]
    [Route(template: "api/IdentityRole")]
    public class IdentityRoleController : Controller
    {
        private readonly IApplicationUnitOfWork _uow;

        public IdentityRoleController(IApplicationUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet(template: "FindByNameAsync/{normalizedName}", Name = "IdentityRole_FindByNameAsync")]
        public async Task<IActionResult> FindByNameAsync(string normalizedName)
        {
            var res = await _uow.IdentityRoles.FindByNameAsync(normalizedName: normalizedName);
            return new ObjectResult(value: res);
        }

        //// GET: api/IdentityRole
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    throw new NotImplementedException();
        //}

        //// GET: api/IdentityRole/5
        //[HttpGet(template: "{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //// POST: api/IdentityRole
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //    throw new NotImplementedException();
        //}

        //// PUT: api/IdentityRole/5
        //[HttpPut(template: "{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    throw new NotImplementedException();
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete(template: "{id}")]
        //public void Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
