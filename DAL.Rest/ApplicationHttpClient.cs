using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DAL.Rest
{
    // analog to dbcontext
    public class ApplicationHttpClient : HttpClient, IDataContext
    {
        public ApplicationHttpClient(string baseAddr) : base()
        {

            if (string.IsNullOrWhiteSpace(value: baseAddr))
            {
                throw new ArgumentNullException(paramName: nameof(baseAddr), message: "Please provide Rest server base address!");
            }

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            BaseAddress = new Uri(uriString: baseAddr);
        }
    }
}
