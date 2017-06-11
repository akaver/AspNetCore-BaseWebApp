using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.RestServer.Filters
{

    /// <summary>
    /// If culture or ui-culture was cpecified in query, set the culture cookie
    /// Since culture should be already set by QueryStringRequestCultureProvider, 
    /// use CultureInfo.CurrentCulture for cookie value. Use default name for cookie: CookieRequestCultureProvider.DefaultCookieName
    /// </summary>
    public class AddCultureCookieFromQueryFilter : IActionFilter
    {
        /// <summary>
        /// The key that contains the culture name.
        /// Defaults to "culture".
        /// </summary>
        public string QueryStringKey { get; set; } = "culture";

        /// <summary>
        /// The key that contains the UI culture name. If not specified or no value is found,
        /// <see cref="QueryStringKey"/> will be used.
        /// Defaults to "ui-culture".
        /// </summary>
        public string UIQueryStringKey { get; set; } = "ui-culture";

        public AddCultureCookieFromQueryFilter(string queryStringKey, string uiQueryStringKey)
        {
            QueryStringKey = queryStringKey;
            UIQueryStringKey = uiQueryStringKey;
        }

        public AddCultureCookieFromQueryFilter()
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string queryCulture = null;
            string queryUICulture = null;

            queryCulture = context.HttpContext.Request.Query[key: QueryStringKey];
            queryUICulture = context.HttpContext.Request.Query[key: UIQueryStringKey];

            // no query specified
            if (queryCulture == null && queryUICulture == null)
            {
                return;
            }



            // add cookie if there was an culture query string.
            // culture should be set by middleware alreade, so just use CultureInfo.CurrentCulture
            context.HttpContext.Response.Cookies.Append(
                key: CookieRequestCultureProvider.DefaultCookieName,
                value: CookieRequestCultureProvider.MakeCookieValue(
                    requestCulture: new RequestCulture(culture: CultureInfo.CurrentCulture)),
                options: new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(years: 1)
                }
                );
        }
    }
}
