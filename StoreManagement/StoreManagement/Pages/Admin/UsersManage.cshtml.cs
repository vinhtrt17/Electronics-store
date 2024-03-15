using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class UsersManageModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IUsersManageServices _usersManageServices;
        private readonly IConfiguration _config;

        public UsersManageModel(WebContext context, IUsersManageServices usersManageServices , IConfiguration config)
        {
            _context = context;
            _usersManageServices = usersManageServices;
            _config = config;
        }
        public int paging { get; set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int totalPage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchValue { get; set; }
        public List<User> users { get; set; }
        public void OnGet()
        {
            paging = Convert.ToInt32(_config.GetSection("PageSettings")["UserManage"]);

            string uname = "";
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["searchId"]))
            {
                uname = HttpContext.Request.Query["searchId"];
            }

            int value = 0;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["index"]))
            {
                value = Convert.ToInt32(HttpContext.Request.Query["index"]);
            }

            users = _usersManageServices.GetUserPaging(value, uname);
            int totalUsers = _usersManageServices.CountUser(uname);
            int totalPage = totalUsers / paging;
            if (totalUsers % paging != 0)
            {
                totalPage++;
            }
            SearchValue = uname;
            //ViewData["TotalPage"] = totalPage;
            //ViewData["CurrentPage"] = value;
            //ViewData["SearchValue"] = uname;
        }
        public IActionResult OnPostUpdateUser(User user)
        {
            User userX = _usersManageServices.GetUserData(user.Username);
            if (!userX.Role.Equals("sa"))
            {
                int status = _usersManageServices.UpdateRole(user);
                if (status == 0)
                {
                    return new JsonResult(new
                    {
                        Status = "Success",
                        Content = "Câòp nhâòt thaÌnh công!"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        Status = "Fail",
                        Content = "Câòp nhâòt thâìt baòi!"
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {
                    Status = "Fail",
                    Content = "Không thêÒ thay ðôÒi quyêÌn admin!"
                });
            }
        }
    }
}
