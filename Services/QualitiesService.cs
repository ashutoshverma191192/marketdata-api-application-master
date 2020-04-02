using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IQualitiesService
    {
        IEnumerable<Qualities> GetAll();
        Qualities GetById(int id);
        Qualities Create(Qualities groups, Users users);
        Qualities Update(Qualities groups, Users users);
        void Delete(int id);
    }
    public class QualitiesService : IQualitiesService
    {
        DataContext _context;
        public QualitiesService(DataContext context)
        {
            _context = context;
        }

        public Qualities Create(Qualities Qualities, Users users)
        {
            // validation
            if (_context.Qualities.Any(x => x.Name == Qualities.Name))
                throw new AppException("Qualities Name \"" + Qualities.Name + "\" is already taken");
            
            if (!_context.Items.Any(x => x.Id == Qualities.ItemId))
                throw new AppException("Item Id \"" + Qualities.ItemId + "\" not found");


            Qualities.ApplicationId = users.ApplicationId;
            Qualities.CreatedBy = users.Id;
            Qualities.CreatedDate = DateTime.Now;
            Qualities.UpdatedBy = users.Id;
            Qualities.UpdatedDate = DateTime.Now;

            _context.Qualities.Add(Qualities);
            _context.SaveChanges();

            return Qualities;
        }

        public void Delete(int id)
        {
            var Qualities = _context.Qualities.Find(id);

            if (Qualities == null)
                throw new AppException("Qualities not found");

            _context.Qualities.Remove(Qualities);
            _context.SaveChanges();
        }

        public IEnumerable<Qualities> GetAll()
        {
            return _context.Qualities;
        }

        public Qualities GetById(int id)
        {
            var Qualities = _context.Qualities.Find(id);

            if (Qualities == null)
                throw new AppException("Qualities not found");

            return Qualities;
        }

        public Qualities Update(Qualities Qualities, Users users)
        {
            if (Qualities is null)
            {
                throw new ArgumentNullException(nameof(Qualities));
            }

            var quality = _context.Qualities.Find(Qualities.Id);

            if (quality == null)
                throw new AppException("quality not found");

            // update quality Name if it has changed
            if (!string.IsNullOrWhiteSpace(Qualities.Name) && Qualities.Name != quality.Name)
            {
                // throw error if the new quality name is already taken
                if (_context.Qualities.Any(x => x.Name == Qualities.Name))
                    throw new AppException("quality Name " + Qualities.Name + " is already taken");

                quality.Name = Qualities.Name;
            }

            // update store ApplicationId if it has changed
            if (Qualities.ApplicationId != 0 && Qualities.ApplicationId != quality.ApplicationId)
            {
                if (!_context.Applications.Any(x => x.Id == Qualities.ApplicationId))
                    throw new AppException("Application Id \"" + Qualities.ApplicationId + "\" is not valid");

                quality.ApplicationId = Qualities.ApplicationId;
            }

            // update store Description if it has changed
            if (!string.IsNullOrWhiteSpace(Qualities.Description) && Qualities.Description != quality.Description)
            {
                quality.Description = Qualities.Description;
            }

            // update store ItemId if it has changed
            if (Qualities.ItemId != 0 && Qualities.ItemId != quality.ItemId)
            {
                if (!_context.Items.Any(x => x.Id == Qualities.ItemId))
                    throw new AppException("ItemId Id \"" + Qualities.ItemId + "\" is not valid");
                quality.ItemId = Qualities.ItemId;
            }


            quality.UpdatedBy = users.Id;
            quality.UpdatedDate = DateTime.Now;

            _context.Qualities.Update(quality);
            _context.SaveChanges();

            return quality;
        }
    }
}
