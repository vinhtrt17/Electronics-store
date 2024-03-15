using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Authentication
{
    public class OrdersDetailsModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IConfiguration _config;

        public OrdersDetailsModel(WebContext context, IProductService productService, IOrderDetailServices orderDetailServices, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _orderDetailServices = orderDetailServices;
            _config = config;
        }
        public List<OrderDetail> orderDetails { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
        public void OnGet(int id)
        {
            string json = HttpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }

            if (userX != null)
            {
                orderDetails = _orderDetailServices.GetOrderDetailsByOId(id);
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    products.Add(_productService.GetProductByPid(orderDetail.Pid));
                }
            }
            else
            {
                Response.Redirect("/HomePage/Index");
            }
        }
    }
}
