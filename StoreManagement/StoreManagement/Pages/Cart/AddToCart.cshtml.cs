using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class AddToCartModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IConfiguration _config;

        public AddToCartModel(WebContext context, IProductService productService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _config = config;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(int id, int num, int cid, int sid)
        {
            string element = "";
            CookieOptions cookieOptions = new CookieOptions();
            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);
            }

            if (string.IsNullOrEmpty(element))
            {
                element = $"{id}:{num}:{cid}:{sid}";
            }
            else
            {
                element = element + $",{id}:{num}:{cid}:{sid}";
            }

            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            Response.Cookies.Append("cartS", element, cookieOptions);
            return new JsonResult(new { Status = "Success" });
        }

    }
}
