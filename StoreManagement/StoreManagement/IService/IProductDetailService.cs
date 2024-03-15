using Microsoft.EntityFrameworkCore;
using StoreManagement.Models;

namespace StoreManagement.IService
{
    public interface IProductDetailService
    {
        public void AddProductDetails(ProductDetail productDetail);
        public ProductDetail GetProductDetail(string pid);
        public int UpdateProductDetails(ProductDetail productDetail);

    }
}
