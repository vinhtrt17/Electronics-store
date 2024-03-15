using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    public class RemoveProductModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IConfiguration _config;

        public RemoveProductModel(WebContext context, IProductService productService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _config = config;
        }
        public IActionResult OnGet(string id)
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
