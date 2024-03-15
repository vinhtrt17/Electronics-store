using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;
using StoreManagement.Services;
using System.Net.NetworkInformation;

namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class AddNewProductModel : PageModel
    {
        private readonly WebContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly IColorDetailServices _colorDetailServices;
        private readonly IStorageDetailServices _storageDetailServices;


        public AddNewProductModel(WebContext context, ICategoryService categoryService, IProductService productService ,IProductDetailService productDetailService,IColorDetailServices colorDetailServices,IStorageDetailServices storageDetailServices)
        {
            _context = context;
            _categoryService = categoryService;
            _productService = productService;
            _productDetailService = productDetailService;
            _colorDetailServices = colorDetailServices;
            _storageDetailServices = storageDetailServices;
        }
        public List<Category> categories { get; set; }
        public void OnGet()
        {
            categories = _categoryService.GetListCategories();
        }

        public IActionResult OnPost(Product product,ProductDetail productDetail ,string colors,  string storages)
        {
            categories = _categoryService.GetListCategories();

            List<string> color = JsonConvert.DeserializeObject<List<string>>(colors);
            List<string> storage = JsonConvert.DeserializeObject<List<string>>(storages);

            _productService.AddNewProduct(product);
            _productDetailService.AddProductDetails(productDetail);
            _colorDetailServices.AddColorDetails(color, product.Pid);
            _storageDetailServices.AddStorageDetails(storage, product.Pid);

            var success = new JsonResult(new { Status = "Success", Content = "Thêm thaÌnh công!" });
            return success;
        }
    }
}
