using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StoreManagement.Models;

namespace StoreManagement.Middleware
{
    public class CheckAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string ADMIN = "sa";
            string USER = "us";

            string json = httpContext.Session.GetString("user");
            User user = null;
            if (!string.IsNullOrEmpty(json))
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }

            string path = httpContext.Request.Path;

            if (path.StartsWith("/Admin") == true || path.StartsWith("/admin") == true)
            {
                if (user != null)
                {
                    if (user.Role.Equals(ADMIN))
                    {
                        //Cho request đi qua
                        await _next(httpContext);
                    }
                    else if (user.Role.Equals(USER))
                    {
                        //Chuyển hướng sang đăng nhập
                        httpContext.Response.Redirect("/Authentication/Authentications?code=403");
                    }
                }
                else
                {
                    //Chuyển hướng sang đăng nhập
                    httpContext.Response.Redirect("/Authentication/Authentications");
                } 
            }
            else
            {
                // Thiết lập Header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });
                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);

            }
        }
    }
}
