using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class OrderService : IOrderServices
    {
        private readonly WebContext _context;


        public OrderService(WebContext context)
        {
            _context = context;
        }
        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetTopValueOrder(int take)
        {
            return _context.Orders.OrderByDescending(x => x.Total).Take(take).ToList();
        }

        public int AddOrder(User user, Cart cart, string description)
        {
            _context.Orders.Add(new Order
            {
                Uname = user.Username,
                Orderdate = DateTime.Now,
                Total = cart.GetTotalMoney()
            });
            _context.SaveChanges();
            Order order = _context.Orders.Where(x => x.Uname.Equals(user.Username)).OrderByDescending(x => x.Id).FirstOrDefault();

            foreach (Item i in cart.Items)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    Oid = order.Id,
                    Pid = i.products.Pid,
                    Quantity = i.quantity,
                    Color = i.colorDetail.Color,
                    Storage = i.storageDetail.Storage,
                    Price = i.products.Price,
                    TotalPrice = i.quantity * i.products.Price,
                    Status = "pd",
                    Description = description
                });
                _context.SaveChanges();

                Product product = _context.Products.FirstOrDefault(x => x.Pid == i.products.Pid);
                if (product != null)
                {
                    product.Amount = product.Amount - i.quantity;
                }

                _context.SaveChanges();

            }
            return 1;
        }

        public List<Order> GetOrderByUname(string uname)
        {
            return _context.Orders.Where(x => x.Uname.Equals(uname)).ToList();
        }
    }
}
