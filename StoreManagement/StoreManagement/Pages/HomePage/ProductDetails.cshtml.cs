using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.HomePage
{
    [IgnoreAntiforgeryToken]
    public class ProductDetailsModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly IColorDetailServices _colorDetailServices;
        private readonly IStorageDetailServices _storageDetailServices;
        private readonly IConfiguration _config;

        public ProductDetailsModel(WebContext context, IProductService productService, IColorDetailServices colorDetailServices, IStorageDetailServices storageDetailServices, IProductDetailService productDetailService, IConfiguration config)
        {
            _context = context;
            _productService = productService;
            _colorDetailServices = colorDetailServices;
            _storageDetailServices = storageDetailServices;
            _productDetailService = productDetailService;
            _config = config;
        }

        public Product product { get; set; }
        public ProductDetail productDetail { get; set; }
        public List<ColorDetail> colors { get; set; }
        public List<StorageDetail> storages { get; set; }

        public void OnGet(string id)
        {
            product = _productService.GetProductByPid(id);
            productDetail = _productDetailService.GetProductDetail(id);
            colors = _colorDetailServices.GetColorDetail(id);
            storages = _storageDetailServices.GetStorageDetails(id);

            if (productDetail == null || colors == null || storages == null)
            {
                Redirect("/Error/Index/500");
            }
        }
    }
}
