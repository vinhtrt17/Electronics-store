using StoreManagement.Models;

namespace StoreManagement.IService
{
    public interface IOrderDetailServices
    {
        public List<OrderDetail> GetOrderDetailsByOId(int oid);

    }
}
