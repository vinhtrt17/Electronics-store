using StoreManagement.Models;
using System.Security.Principal;

namespace StoreManagement.IService
{
    public interface IUsersManageServices
    {
        public User Login(string user, string pass);
        public int Register(User user);
        public List<User> GetAll();
        public int CountUser(string uname);
        public List<User> GetUserPaging(int take, string uname);
        public User GetUserData(string userName);
        public int UpdateRole(User user);
        public User CheckExist(string username);
        public User GetUserByOId(int oid);
        public int UpdateProfile(User user);

    }
}
