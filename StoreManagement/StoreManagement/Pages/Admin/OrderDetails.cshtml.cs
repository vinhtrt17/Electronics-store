using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    public class OrderDetailsModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IUsersManageServices _usersManageServices;
        private readonly IProductService _productService;
        private readonly IConfiguration _config;

        public OrderDetailsModel(WebContext context, IOrderDetailServices orderDetailServices, IUsersManageServices usersManageServices,IProductService productService, IConfiguration config)
        {
            _context = context;
            _orderDetailServices = orderDetailServices;
            _usersManageServices = usersManageServices;
            _productService = productService;
            _config = config;
        }

        public List<OrderDetail> orderDetails { get; set; }
        public List<Product> products { get;set; } = new List<Product>();
        public User user { get; set; }
        public void OnGet(int id)
        {
            orderDetails = _orderDetailServices.GetOrderDetailsByOId(id);
            user = _usersManageServices.GetUserByOId(id);

            foreach (OrderDetail orderDetail in orderDetails)
            {
                products.Add(_productService.GetProductByPid(orderDetail.Pid));
            }
        }
    }
}
