using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class OrderDetailService : IOrderDetailServices
    {
        private readonly WebContext _context;


        public OrderDetailService(WebContext context)
        {
            _context = context;
        }


        public List<OrderDetail> GetOrderDetailsByOId(int oid)
        {
            return _context.OrderDetails.Where(x => x.Oid == oid).ToList();
        }
    }
}
