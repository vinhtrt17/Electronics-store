using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class ColorDetailServices : IColorDetailServices
    {
        private readonly WebContext _context;
        public ColorDetailServices(WebContext context)
        {
            _context = context;
        }
        public List<ColorDetail> GetAllColor()
        {
            return _context.ColorDetails.ToList();
        }
        public void AddColorDetails(List<string> colors, string pid)
        {
            List<ColorDetail> colorsList = new List<ColorDetail>();
            foreach (string color in colors)
            {
                colorsList.Add(new ColorDetail { Pid = pid, Color = color });
            };
            _context.AddRange(colorsList);
            _context.SaveChanges();
        }

        public List<ColorDetail> GetColorDetail(string pid)
        {
            return _context.ColorDetails.Where(x => x.Pid.Equals(pid)).ToList();
        }

        public int RemoveColor(int id)
        {
            ColorDetail color = _context.ColorDetails.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                _context.ColorDetails.Remove(color);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateColorDetails(List<ColorDetail> colorDetails)
        {
            try
            {
                foreach (ColorDetail colorDetail in colorDetails)
                {
                    ColorDetail pd = _context.ColorDetails.Where(x => x.Id == colorDetail.Id && x.Pid == colorDetail.Pid).FirstOrDefault();
                    pd.Color = colorDetail.Color;
                }
                //context.UpdateRange(colorDetails);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public ColorDetail GetColorById(int cid)
        {
            return _context.ColorDetails.FirstOrDefault(x => x.Id == cid);
        }
    }
}
