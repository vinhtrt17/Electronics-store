using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class ProductDetailServices : IProductDetailService
    {
        private readonly WebContext _context;
        public ProductDetailServices(WebContext context)
        {
            _context = context;
        }
        public void AddProductDetails(ProductDetail productDetail)
        {
            _context.Add(productDetail);
            _context.SaveChanges();
        }

        public ProductDetail GetProductDetail(string pid)
        {
            return _context.ProductDetails.Where(x => x.Pid.Equals(pid)).FirstOrDefault();
        }

        public int UpdateProductDetails(ProductDetail productDetail)
        {
            ProductDetail pd = _context.ProductDetails.FirstOrDefault(x => x.Pid == productDetail.Pid);
            try
            {
                pd.Screen = productDetail.Screen;
                pd.Os = productDetail.Os;
                pd.Rearcam = productDetail.Rearcam;
                pd.Frontcam = productDetail.Frontcam;
                pd.Soc = productDetail.Soc;
                pd.Ram = productDetail.Ram;
                pd.Sim = productDetail.Sim;
                pd.Battery = productDetail.Battery;
                //context.Update(productDetail);
                _context.SaveChanges();
                return 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
