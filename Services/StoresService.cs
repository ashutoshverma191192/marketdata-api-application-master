using Microsoft.AspNetCore.Builder;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Stores;

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

            if (!_context.CityMasters.Any(x => x.Id == stores.CityId))
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
            var store = _context.Stores.Find(id);
            if (store == null)
                throw new AppException("store not found");

            _context.Stores.Remove(store);
            _context.SaveChanges();
        }

        public IEnumerable<Stores> GetAll()
        {
            return _context.Stores;
        }

        public Stores GetById(int id)
        {
            var store = _context.Stores.Find(id);
            if (store == null)
                throw new AppException("store not found");

            return store;
        }

        public Stores Update(Stores stores)
        {
            var store = _context.Stores.Find(stores.Id);

            if (store == null)
                throw new AppException("store not found");

            // update store name if it has changed
            if (!string.IsNullOrWhiteSpace(stores.Name) && stores.Name != store.Name)
            {
                // throw error if the new store name is already taken
                if (_context.Stores.Any(x => x.Name == stores.Name))
                    throw new AppException("store Name " + stores.Name + " is already Registered");

                store.Name = stores.Name;
            }
            
            // update store AddressLine1 if it has changed
            if (!string.IsNullOrWhiteSpace(stores.AddressLine1) && stores.AddressLine1 != store.AddressLine1)
            {
                store.AddressLine1 = stores.AddressLine1;
            }

            // update store AddressLine2 if it has changed
            if (!string.IsNullOrWhiteSpace(stores.AddressLine2) && stores.AddressLine2 != store.AddressLine2)
            {
                store.AddressLine2 = stores.AddressLine2;
            }

            // update store AddressLine2 if it has changed
            if (stores.ApplicationId != 0  && stores.ApplicationId != store.ApplicationId)
            {
                if(!_context.Applications.Any(x => x.Id == stores.ApplicationId))
                    throw new AppException("Application Id \"" + stores.ApplicationId + "\" is not valid");

                store.ApplicationId = stores.ApplicationId;
            }

            // update store cityId if it has changed
            if (stores.CityId != 0 && stores.CityId != store.CityId)
            {
                if (!_context.CityMasters.Any(x => x.Id == stores.CityId))
                    throw new AppException("City Id \"" + stores.CityId + "\" is not valid");

                store.CityId = stores.CityId;
            }

            // update store ContactPerson if it has changed
            if (!string.IsNullOrWhiteSpace(stores.ContactPerson) && stores.ContactPerson != store.ContactPerson)
            {
                store.ContactPerson = stores.ContactPerson;
            }

            // update store PhoneNumber if it has changed
            if (!string.IsNullOrWhiteSpace(stores.PhoneNumber) && stores.PhoneNumber != store.PhoneNumber)
            {
                store.PhoneNumber = stores.PhoneNumber;
            }

            // update store PhoneNumber2 if it has changed
            if (!string.IsNullOrWhiteSpace(stores.PhoneNumber2) && stores.PhoneNumber2 != store.PhoneNumber2)
            {
                store.PhoneNumber2 = stores.PhoneNumber2;
            }

            // update store PostalCode if it has changed
            if (stores.PostalCode != 0 && stores.PostalCode != store.PostalCode)
            {
                store.PostalCode = stores.PostalCode;
            }

            // update store Remark if it has changed
            if (!string.IsNullOrWhiteSpace(stores.Remark))
            {
                store.Remark = stores.Remark;
            }
            stores.UpdatedDate = System.DateTime.Now;

            _context.Stores.Update(store);
            _context.SaveChanges();

            return store;
        }
    }
}
