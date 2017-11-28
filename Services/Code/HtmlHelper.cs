using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Services.Code
{
    public static class HtmlHelper
    {
        private static string websiteURL = null;
        public static void Init(string websiteURL)
        {
            HtmlHelper.websiteURL = websiteURL;
        }
        public static UrlHelper UrlHelper
        {
            get
            {
                if (HttpContext.Current?.Request?.RequestContext != null)
                    return new UrlHelper(HttpContext.Current.Request.RequestContext);
                else
                    return new UrlHelper(generateContext());

            }
        }

        private static RequestContext generateContext()
        {
            if (websiteURL == null)
                throw new Exception();

            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                var request = new HttpRequest("/", websiteURL, "");
                var response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            return new RequestContext(httpContextBase, routeData);
        }
    }
}
