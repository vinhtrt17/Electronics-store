using StoreManagement.Models;

namespace StoreManagement.IService
{
    public interface IColorDetailServices
    {
        public List<ColorDetail> GetAllColor();
        public void AddColorDetails(List<string> colors, string pid);
        public List<ColorDetail> GetColorDetail(string pid);
        public int RemoveColor(int id);
        public int UpdateColorDetails(List<ColorDetail> colorDetails);
        public ColorDetail GetColorById(int cid);

    }
}
