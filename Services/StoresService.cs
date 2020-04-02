using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IStoreService
    {
        IEnumerable<Stores> GetAll();
        Stores GetById(int id);
        Stores Create(Stores stores, Users users);
        Stores Update(Stores stores);
        void Delete(int id);
    }
    public class StoresService : IStoreService
    {
        private DataContext _context;

        public StoresService(DataContext context)
        {
            _context = context;
        }
        public Stores Create(Stores stores, Users users)
        {
            if (_context.Stores.Any(x => x.Name == stores.Name))
                throw new AppException("Store Name \"" + stores.Name + "\" is already taken");

            if(!_context.CityMasters.Any(x => x.Id == stores.CityId))
                throw new AppException("City Id \"" + stores.CityId + "\" is not valid");

            stores.CreatedDate = DateTime.UtcNow;
            stores.UpdatedDate = DateTime.UtcNow;
            stores.ApplicationId = users.ApplicationId;

            _context.Stores.Add(stores);
            _context.SaveChanges();

            return stores;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stores> GetAll()
        {
            throw new NotImplementedException();
        }

        public Stores GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Stores Update(Stores stores)
        {
            throw new NotImplementedException();
        }
    }
}
