using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class DashboardModel : PageModel
    {

        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IOrderServices _orderServices;
        private readonly IConfiguration _config;

        public DashboardModel(WebContext context,IOrderServices orderServices,IProductService productService ,IConfiguration config)
        {
            _context = context;
            _orderServices = orderServices;
            _productService = productService;
            _config = config;
        }
        public List<Order> orders { get; set; }
        public dynamic products { get; set; }

        public IActionResult OnPostTopSellProduct(int take)
        {
            products = _productService.BestSellProduct(take);
            return new JsonResult(products);
        }

        public IActionResult OnPostTopValueOrders(int take)
        {
            orders = _orderServices.GetTopValueOrder(take);
            return new JsonResult(orders);
        }
    }
}
