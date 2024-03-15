using StoreManagement.IService;
using StoreManagement.Models;

namespace StoreManagement.Services
{
    public class StorageDetailServices : IStorageDetailServices
    {
        private readonly WebContext _context;
        public StorageDetailServices(WebContext context)
        {
            _context = context;
        }
        public List<StorageDetail> GetAllStorage()
        {
            return _context.StorageDetails.ToList();
        }
        public void AddStorageDetails(List<string> storages, string pid)
        {
            List<StorageDetail> storageDetails = new List<StorageDetail>();
            foreach (string storage in storages)
            {
                storageDetails.Add(new StorageDetail { Pid = pid, Storage = storage });
            };
            _context.AddRange(storageDetails);
            _context.SaveChanges();
        }

        public List<StorageDetail> GetStorageDetails(string pid)
        {
            return _context.StorageDetails.Where(x => x.Pid.Equals(pid)).ToList();
        }

        public int RemoveStorage(int id)
        {
            StorageDetail storage = _context.StorageDetails.Where(x => x.Id == id).FirstOrDefault();
            try
            {
                _context.StorageDetails.Remove(storage);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateStorageDetails(List<StorageDetail> storageDetails)
        {
            try
            {
                foreach (StorageDetail storageDetail in storageDetails)
                {
                    StorageDetail sd = _context.StorageDetails.Where(x => x.Id == storageDetail.Id && x.Pid == storageDetail.Pid).FirstOrDefault();
                    sd.Storage = storageDetail.Storage;
                }
                //context.UpdateRange(storageDetails);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public StorageDetail GetStorageById(int cid)
        {
            return _context.StorageDetails.FirstOrDefault(x => x.Id == cid);
        }
    }
}
