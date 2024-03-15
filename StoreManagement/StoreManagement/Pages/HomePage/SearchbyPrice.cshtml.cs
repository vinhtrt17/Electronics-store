using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.HomePage
{
    public class SearchbyPriceModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IConfiguration _config;

        public SearchbyPriceModel(WebContext context, IProductService productService, IOrderDetailServices orderDetailServices, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _orderDetailServices = orderDetailServices;
            _config = config;
        }
        public List<Product> products { get; set; }
        public int paging { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int totalPage { get; set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 0;
        public string[] searchValue { get; set; }
        public void OnGet(string id)
        {
            paging = Convert.ToInt32(_config.GetSection("PageSettings")["Paging"]);

            searchValue = id.Trim().Split("-");
            int min = 0, max = 0;

            if (searchValue.Length > 0)
            {
                min = Convert.ToInt32(searchValue[0]);
                max = Convert.ToInt32(searchValue[1]);
            }

            products = _productService.SearchByPricePaging(min, max, currentPage);
            int totalProducts = _productService.GetAllProductsByPrice(min, max);
            totalPage = totalProducts / paging;
            if (totalProducts % paging != 0)
            {
                totalPage++;
            }

            ViewData["SearchValue"] = id;
            ViewData["SearchTitle"] = $"{min}tr đến {max}tr";
        }
    }
}
