using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
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
            orders = _orderServices.GetAllOrders();
        }
    }
}

