using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Authentication
{
    [IgnoreAntiforgeryToken]
    public class ProfileModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IUsersManageServices _usersManageServices;
        private readonly IConfiguration _config;

        public ProfileModel(WebContext context, IUsersManageServices usersManageServices, IConfiguration config)
        {
            _context = context;
            _usersManageServices = usersManageServices;
            _config = config;
        }
        public void OnGet()
        {
        }
        public void OnPost(User user)
        {
            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }
            user.Username = userX.Username;
            int status = _usersManageServices.UpdateProfile(user);
            if (status == 0)
            {
                User newData = _usersManageServices.GetUserData(user.Username);
                string jsons = JsonConvert.SerializeObject(newData);
                HttpContext.Session.SetString("user", jsons);
                ViewData["Message"] = "C‚Úp nh‚Út thaÃnh cÙng!";
            }
            else
            {
                ViewData["Message"] = "C‚Úp nh‚Út th‚Ït baÚi!";

            }
        }
    }
}
