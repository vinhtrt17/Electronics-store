using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Pages.Admin
{
    [IgnoreAntiforgeryToken]
    public class ProductDetailsModel : PageModel
    {
        private readonly WebContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductDetailService _productDetailService;
        private readonly IColorDetailServices _colorDetailServices;
        private readonly IStorageDetailServices _storageDetailServices;
        private readonly IConfiguration _config;

        public ProductDetailsModel(WebContext context, IProductService productService, IConfiguration config,ICategoryService categoryService, IProductDetailService productDetailService,IColorDetailServices colorDetailServices , IStorageDetailServices storageDetailServices)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _config = config;
            _productDetailService = productDetailService;
            _colorDetailServices = colorDetailServices;
            _storageDetailServices = storageDetailServices;
        }

        public List<Category> categories { get; set; }
        public List<ColorDetail> colors { get; set; }
        public List<StorageDetail> storages { get; set; }
        public ProductDetail productDetail { get; set; }
        public Product product { get; set; }
        public void OnGet(string id)
        {
            categories = _categoryService.GetListCategories();
            productDetail = _productDetailService.GetProductDetail(id);
            product = _productService.GetProductByPid(id);
            colors = _colorDetailServices.GetColorDetail(id);
            storages = _storageDetailServices.GetStorageDetails(id);
        }
        public IActionResult OnPostRemoveColor(int id)
        {
            int status = _colorDetailServices.RemoveColor(id);
            if (status == 1)
            {
                var success = new JsonResult(new { Status = "Success", Content = "Xoìa thaÌnh công!" });
                return success;
            }
            else
            {
                var fail = new JsonResult(new { Status = "Fail", Content = "Xoìa thâìt baòi!" });
                return fail;
            }
        }

        public IActionResult OnPostRemoveStorage(int id)
        {
            int status = _storageDetailServices.RemoveStorage(id);
            if (status == 1)
            {
                return new JsonResult(new { Status = "Success", Content = "Xoìa thaÌnh công!" });
            }
            else
            {
                return new JsonResult(new { Status = "Fail", Content = "Xoìa thâìt baòi!" });
            }
        }

        public IActionResult OnPostUpdateProduct(string oldColors, string oldStorages, string newStorages, string newColors, Product product, ProductDetail productDetail)
        {

            List<ColorDetail> colorDetails = JsonConvert.DeserializeObject<List<ColorDetail>>(oldColors);
            List<StorageDetail> storageDetails = JsonConvert.DeserializeObject<List<StorageDetail>>(oldStorages);

            List<string> color = JsonConvert.DeserializeObject<List<string>>(newColors);
            List<string> storage = JsonConvert.DeserializeObject<List<string>>(newStorages);

            int updateProduct = _productService.UpdateProduct(product);
            int updateDetails = _productDetailService.UpdateProductDetails(productDetail);
            int updateColor = _colorDetailServices.UpdateColorDetails(colorDetails);
            int updateStorage = _storageDetailServices.UpdateStorageDetails(storageDetails);

            if (color != null && color.Count > 0)
            {
                _colorDetailServices.AddColorDetails(color, product.Pid);
            }

            if (storage != null && storage.Count > 0)
            {
                _storageDetailServices.AddStorageDetails(storage, product.Pid);
            }

            if (updateProduct == 1 && updateDetails == 1 && updateColor == 1 && updateStorage == 1)
            {
                return new JsonResult(new { Status = "Success", Content = "Cập nhật thành công !" });
            }
            else
            {
                return new JsonResult(new { Status = "Fail", Content = "Cập nhật thất bại!" });
            }

        }
    }
}
