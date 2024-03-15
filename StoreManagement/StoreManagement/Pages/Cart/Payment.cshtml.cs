using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class PaymentModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IColorDetailServices _colorDetailServices;
        private readonly IStorageDetailServices _storageDetailServices;
        private readonly IOrderServices _orderServices;
        private readonly IConfiguration _config;

        public PaymentModel(WebContext context, IProductService productService, IColorDetailServices colorDetailServices, IStorageDetailServices storageDetailServices,IOrderServices orderServices, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _colorDetailServices = colorDetailServices;
            _storageDetailServices = storageDetailServices;
            _orderServices = orderServices;
            _config = config;
        }
        public List<Product> products { get; set; }
        public List<ColorDetail> colorDetail { get; set; }
        public List<StorageDetail> storageDetail { get; set; }
        public Models.Cart cart { get; set; }
        User user { get;set; }
        public IActionResult OnGet()
        {
                List<Product> products = _productService.GetListProduct();
                List<ColorDetail> colorDetail = _colorDetailServices.GetAllColor();
                List<StorageDetail> storageDetail = _storageDetailServices.GetAllStorage();

                string element = "";

                if (Request.Cookies["cartS"] != null)
                {
                    element += Request.Cookies["cartS"];
                }

                cart = new Models.Cart(element, products, colorDetail, storageDetail);

                if (cart.Items.Count <= 0)
                {
                    ViewData["CartSize"] = "hidden";
                }
                else
                {
                    ViewData["CartSize"] = "display";
                }
            
            string json = HttpContext.Session.GetString("user");
            if (json != null)
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }
            if (user != null)
            {
                return Page();
            }
            else
            {
                return Redirect("/Authentication/Authentications");
            }

        }

        public IActionResult OnPost(string description)
        {
            products = _productService.GetListProduct();
            colorDetail = _colorDetailServices.GetAllColor();
            storageDetail = _storageDetailServices.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            cart = new Models.Cart(element, products, colorDetail, storageDetail);

            string? json = HttpContext.Session.GetString("user");
            User user = null;
            if (json != null)
                user = JsonConvert.DeserializeObject<User>(json);

            if (user != null)
            {
                _orderServices.AddOrder(user, cart, description);

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);

                return Redirect("/HomePage/Index");
            }
            else
            {
                return Redirect("/Authentication/Authentications");
            }
        }
    }
}
