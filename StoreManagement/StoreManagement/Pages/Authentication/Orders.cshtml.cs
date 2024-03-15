using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Authentication
{
    public class OrdersModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IOrderServices _orderServices;
        private readonly IConfiguration _config;

        public OrdersModel(WebContext context, IOrderServices orderServices, IConfiguration config)
        {
            _context = context;
            _orderServices = orderServices;
            _config = config;
        }
        public List<Order> orders { get; set; }
        public void OnGet()
        {
            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }
            if (userX != null)
            {
                orders = _orderServices.GetOrderByUname(userX.Username);
            }
            else
            {
                Response.Redirect("/HomePage/Index");
            }
        }
    }
}
