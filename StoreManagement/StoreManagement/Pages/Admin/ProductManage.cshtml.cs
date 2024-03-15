using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;
using Newtonsoft.Json;
using System.Text.Json;



namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class ProductManageModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IConfiguration _config;

        public ProductManageModel(WebContext context, IProductService productService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _config = config;
        }
        public int paging { get; set; }
        [BindProperty(SupportsGet = true)]
        public int currentPage { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int totalPage { get; set; }
        public List<Product> products { get; set; }


        public void OnGet()
        {
            int totalProducts = _productService.CountAllProduct();
            products = _productService.GetListProductPaging(currentPage);
            paging = Convert.ToInt32(_config.GetSection("PageSettings")["Paging"]);


            int totalPage = totalProducts / paging;
            if (totalProducts % paging != 0)
            {
                totalPage++;
            }

            ViewData["TotalPage"] = totalPage;
            ViewData["CurrentPage"] = currentPage;
        }


        public IActionResult OnPostDeleteProduct(string id)
        {
            int status = _productService.RemoveProduct(id);
            if (status == 1)
            {
                var success = new JsonResult(new { Status = "Success", Content = "Xoá Thành Công!" });
                return success;
            }
            else
            {
                var fail = new JsonResult(new { Status = "Fail", Content = "Xoìa thâìt baòi!" });
                return fail;
            }
        }
    }
}
