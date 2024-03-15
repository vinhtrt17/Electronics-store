using Microsoft.AspNetCore.SignalR;
using StoreManagement.Models;

namespace StoreManagement.Pages
{
    public class StoreHub : Hub
    {
        public async Task UpdatePrice(string name, string price)
        {
            await Clients.All.SendAsync("ReceivePriceUpdate", name, price);
        }
        public async Task ClrOrder(string uid)
        {
            await Clients.User(uid).SendAsync("ReceiveClearOrder");
        } 
        public async Task AddOrder( )
        {
            await Clients.All.SendAsync("ReceiveAddOrder");
        }
    }
}
