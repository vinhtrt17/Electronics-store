using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.HomePage
{
    [IgnoreAntiforgeryToken]
    public class SearchByNameModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;

        public SearchByNameModel(WebContext context, IProductService productService, ICategoryService categoryService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _config = config;
        }
        public List<Product> products { get; set; }
        public int paging { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int totalPage { get; set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 0;
        public Category category { get; set; }

        public string SearchByName { get; set; }
        public void OnGet(string value)
        {
            paging = Convert.ToInt32(_config.GetSection("PageSettings")["Paging"]);
            if (!string.IsNullOrEmpty(value))
            {
                SearchByName = value;
            }
            int items = _productService.GetAllProductsByName(SearchByName);
            products = _productService.SearchByName(SearchByName, currentPage);
            totalPage = items / paging;
            if (items % paging != 0)
            {
                totalPage++;
            }
            ViewData["SearchData"] = SearchByName;
        }
    }
}
