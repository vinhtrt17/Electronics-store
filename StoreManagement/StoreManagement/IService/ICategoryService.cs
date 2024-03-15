using StoreManagement.Models;

namespace StoreManagement.IService
{
    public interface ICategoryService
    {
        public List<Category> GetListCategories();
        public Category GetCategoryById(int id);
    }
}
