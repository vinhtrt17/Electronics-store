using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Authentication
{
    [IgnoreAntiforgeryToken]
    public class AuthenticationsModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IUsersManageServices _usersManageServices;
        private readonly IConfiguration _config;

        public AuthenticationsModel(WebContext context, IUsersManageServices usersManageServices, IConfiguration config)
        {
            _context = context;
            _usersManageServices = usersManageServices;
            _config = config;
        }
        public IActionResult OnPostLogin(User user)
        {
            User Login = _usersManageServices.Login(user.Username, user.Password);
            string json = JsonConvert.SerializeObject(Login);

            if (Login != null)
            {
                HttpContext.Session.SetString("user", json);
                if (Login.Role.Equals("sa"))
                {
                    var message = new
                    {
                        Status = "Success",
                        Content = "Pass",
                        IsAdmin = "true"
                    };
                    return new JsonResult(message);
                }
                else if (Login.Role.Equals("us"))
                {
                    var message = new
                    {
                        Status = "Success",
                        Content = "Pass",
                        IsAdmin = "false"
                    };
                    return new JsonResult(message);
                }
                else
                {
                    var message = new
                    {
                        Status = "Fail",
                        Content = "Bạn không có quyền truy cập"
                    };
                    return new JsonResult(message);
                }
            }
            else
            {
                var message = new
                {
                    Status = "Fail",
                    Content = "Sai tài khoản hoặc mật khẩu!"
                };
                return new JsonResult(message);
            }
        }
        public IActionResult OnPostRegister(User user)
        {
            int status = _usersManageServices.Register(user);
            if (status == 0)
            {
                var message = new
                {
                    Status = "Success",
                    Content = "Đăng kí thành công!",
                };
                return new JsonResult(message);
            }
            else
            {
                var message = new
                {
                    Status = "Fail",
                    Content = "Tên đăng nhập đã tồn tại!"
                };
                return new JsonResult(message);
            }
        }

        public IActionResult OnGetLogout()
        {
            var session = HttpContext.Session;
            session.Clear();
            return Redirect("/HomePage/Index");
        }
    }
}
