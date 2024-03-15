using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Cart
{
    [IgnoreAntiforgeryToken]
    public class ShowCartModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IColorDetailServices _colorDetailServices;
        private readonly IStorageDetailServices _storageDetailServices;
        private readonly IConfiguration _config;

        public ShowCartModel(WebContext context, IProductService productService,IColorDetailServices colorDetailServices,IStorageDetailServices storageDetailServices , IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _colorDetailServices = colorDetailServices;
            _storageDetailServices = storageDetailServices;
            _config = config;
        }
        public List<Product> products { get; set; }
        public List<ColorDetail> colorDetails { get; set; }
        public List<StorageDetail> storageDetails { get; set; }
        public Models.Cart cart { get; set; }
        public void OnGet()
        {
            products = _productService.GetListProduct();
            colorDetails = _colorDetailServices.GetAllColor();
            storageDetails = _storageDetailServices.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            cart = new Models.Cart(element, products, colorDetails, storageDetails);

            Console.WriteLine("Cart" + " " + element);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }
        }

        public IActionResult OnGetAdd()
        {
            products = _productService.GetListProduct();
            colorDetails = _colorDetailServices.GetAllColor();
            storageDetails = _storageDetailServices.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            cart = new Models.Cart(element, products, colorDetails, storageDetails);

            Console.WriteLine("Cart" + " " + element);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }
            return new JsonResult(new { status = "Success" });
        }

        public IActionResult OnGetClear()
        {
            products = _productService.GetListProduct();
            colorDetails = _colorDetailServices.GetAllColor();
            storageDetails = _storageDetailServices.GetAllStorage();

            string element = "";

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
            }

            cart = new Models.Cart(element, products, colorDetails, storageDetails);

            Console.WriteLine("Cart" + " " + element);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }
            return new JsonResult(new { status = "Success" });
        }

        public IActionResult OnPostQuantityProcess(int id, int num, int cid, int sid)
        {
            products = _productService.GetListProduct();
            colorDetails = _colorDetailServices.GetAllColor();
            storageDetails = _storageDetailServices.GetAllStorage();

            string element = "";
            CookieOptions cookieOptions = new CookieOptions();

            if (Request.Cookies["cartS"] != null)
            {
                element += Request.Cookies["cartS"];
                cookieOptions.Expires = DateTime.Now;
                Response.Cookies.Append("cartS", "", cookieOptions);
            }

            cart = new Models.Cart(element, products, colorDetails, storageDetails);
            Product product = _productService.GetProductById(id);
            ColorDetail colorDetail1 = _colorDetailServices.GetColorById(cid);
            StorageDetail storageDetail1 = _storageDetailServices.GetStorageById(sid);

            int store = product.Amount;

            if (num == -1 && cart.GetQuantityById(id, cid, sid) <= 1)
            {
                cart.Remove(id, cid, sid);
            }
            else if (num == 0)
            {
                cart.Remove(id, cid, sid);

            }
            else
            {
                if (num == 1 && cart.GetQuantityById(id, cid, sid) >= store)
                {
                    num = 0;
                }

                Item item = new Item(product, colorDetail1, storageDetail1, num);
                cart.AddItem(item);
            }

            List<Item> items = cart.Items;
            element = "";
            if (items.Count > 0)
            {
                element = $"{items[0].products.Id}:{items[0].quantity}:{items[0].colorDetail.Id}:{items[0].storageDetail.Id}";
                for (int i = 1; i < items.Count; i++)
                {
                    element += $",{items[i].products.Id}:{items[i].quantity}:{items[i].colorDetail.Id}:{items[i].storageDetail.Id}";
                }
            }

            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            Response.Cookies.Append("cartS", element, cookieOptions);

            if (cart.Items.Count <= 0)
            {
                ViewData["CartSize"] = "hidden";
            }
            else
            {
                ViewData["CartSize"] = "display";
            }

            return new JsonResult(new { Status = "Success" });
        }


    }
}
