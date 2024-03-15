using StoreManagement.Models;

namespace StoreManagement.IService
{
    public interface IOrderServices
    {
        public List<Order> GetAllOrders();
        public List<Order> GetTopValueOrder(int take);
        public int AddOrder(User user, Cart cart, string description);
        public List<Order> GetOrderByUname(string uname);
    }
}
