using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AsyncTestController : Controller
    {
        private Random r = new Random();

        public async Task<IActionResult> Index()
        {
            var vm = new AsyncTestIndexViewModel();
            vm.RequestStartAt = DateTime.Now;


            var tasks = new List<Task<HttpResponseMessage>>();



            for (int i = 0; i < 5; i++)
            {
                var client = new HttpClient();
                var url = "http://localhost:29014/api/AsyncApi?r=" + r.Next().ToString();
                vm.Res.Add(url);
                tasks.Add(client.GetAsync(url));
            }

            foreach (var t in tasks)
            {
                vm.Res.Add(t.IsCompleted.ToString());
            }

            foreach (var t in tasks)
            {
                await t;
            }
            // await Task.WhenAll(tasks);

            var responses = new List<Task<string>>();
            foreach (var task in tasks)
            {
                responses.Add(task.Result.Content.ReadAsStringAsync());
            }

            vm.Res.AddRange((await Task.WhenAll(responses)).ToList());

            vm.RequestDoneAt = DateTime.Now;
            return View(vm);
        }
    }
}