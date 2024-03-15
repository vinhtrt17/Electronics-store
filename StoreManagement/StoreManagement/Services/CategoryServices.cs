using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly WebContext _context;
        public CategoryServices(WebContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(x => x.Cid == id).FirstOrDefault();
        }

        public List<Category> GetListCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
