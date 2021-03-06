using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/AsyncApi")]
    public class AsyncApiController : Controller
    {
        private Random r = new Random();


        private static string x;

        private static int d = 1;

        // GET: api/AsyncApi
        [HttpGet]
        public async Task<string> Get()
        {
            var delay = d;
            d++;
            await Task.Delay(delay * 1000);
            x = delay + " " + x;

            return x;
        }

        // GET: api/AsyncApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/AsyncApi
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/AsyncApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
