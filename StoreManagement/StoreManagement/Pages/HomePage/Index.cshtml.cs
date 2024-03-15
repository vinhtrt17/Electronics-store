using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.HomePage
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;

        public IndexModel(WebContext context, IProductService productService,ICategoryService categoryService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _config = config;
        }
        public List<Product> products { get; set; } 
        public int paging { get; set; } = 0;
        [BindProperty (SupportsGet =true)]
        public int totalPage { get; set; }
        [BindProperty (SupportsGet =true)]
        public int currentPage { get; set; }=0;
        public Category category { get; set; }

        public string SearchByName { get; set; }
        public void OnGet(int id )
        {
            paging = Convert.ToInt32(_config.GetSection("PageSettings")["Paging"]);
            category = _categoryService.GetCategoryById(id);
            products = _productService.SearchByCategoryPaging(id, currentPage);
            int totalProducts = _productService.CountAllProductsByCateId(id);

             totalPage = totalProducts / paging;
            if (totalProducts % paging != 0)
            {
                totalPage++;
            }
        }

        public IActionResult OnGetLoadCategory()
        {
            List<Category> categories = _categoryService.GetListCategories();
            var cateJson =  JsonConvert.SerializeObject(categories);
            return new JsonResult(cateJson);
        }
    }
}
