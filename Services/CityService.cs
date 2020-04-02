using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ICityService
    {
        IEnumerable<CityMasters> GetAll();
        CityMasters GetById(int id);
        CityMasters Create(CityMasters cityMasters);
        void  Update(CityMasters cityMasters);
        void Delete(int id);
    }
    public class CityService : ICityService
    {
        private DataContext _context;

        public CityService(DataContext context)
        {
            _context = context;
        }
        public CityMasters Create(CityMasters cityMasters)
        {
            // validation
            if (_context.CityMasters.Any(x => x.Name == cityMasters.Name))
                throw new AppException("City Name \"" + cityMasters.Name + "\" is already taken");

            _context.CityMasters.Add(cityMasters);
            _context.SaveChanges();

            return cityMasters;
        }

        public void Delete(int id)
        {
            var city = _context.CityMasters.Find(id);
            if(city == null)
                throw new AppException("city not found");

            _context.CityMasters.Remove(city);
            _context.SaveChanges();
        }

        public IEnumerable<CityMasters> GetAll()
        {
            return _context.CityMasters;
        }

        public CityMasters GetById(int id)
        {
            var city = _context.CityMasters.Find(id);
            if (city == null)
                throw new AppException("city not found");

            return city;
        }

        public void Update(CityMasters cityMasters)
        {
            var city = _context.CityMasters.Find(cityMasters.Id);

            if (city == null)
                throw new AppException("city not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(cityMasters.Name) && cityMasters.Name != city.Name)
            {
                // throw error if the new city name is already taken
                if (_context.CityMasters.Any(x => x.Name == cityMasters.Name))
                    throw new AppException("City Name " + cityMasters.Name + " is already Registered");

                city.Name = cityMasters.Name;
            }

            _context.CityMasters.Update(city);
            _context.SaveChanges();
        }
    }
}
