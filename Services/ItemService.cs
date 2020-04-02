using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IItemService
    {
        IEnumerable<Items> GetAll();
        Items GetById(int id);
        Items Create(Items groups, Users users);
        Items Update(Items groups, Users users);
        void Delete(int id);
    }
    public class ItemService : IItemService
    {
        DataContext _context;
        public ItemService(DataContext context)
        {
            _context = context;
        }

        public Items Create(Items Items, Users users)
        {
            // validation
            if (_context.Items.Any(x => x.Name == Items.Name))
                throw new AppException("Items Name \"" + Items.Name + "\" is already taken");

            Items.ApplicationId = users.ApplicationId;
            Items.CreatedBy = users.Id;
            Items.CreatedDate = DateTime.Now;
            Items.UpdatedBy = users.Id;
            Items.UpdatedDate = DateTime.Now;

            _context.Items.Add(Items);
            _context.SaveChanges();

            return Items;
        }

        public void Delete(int id)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
                throw new AppException("Items not found");

            _context.Items.Remove(Items);
            _context.SaveChanges();
        }

        public IEnumerable<Items> GetAll()
        {
            return _context.Items;
        }

        public Items GetById(int id)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
                throw new AppException("Items not found");

            return Items;
        }

        public Items Update(Items Items, Users users)
        {
            if (Items is null)
            {
                throw new ArgumentNullException(nameof(Items));
            }

            var item = _context.Items.Find(Items.Id);

            if (item == null)
                throw new AppException("Item not found");

            // update Item Name if it has changed
            if (!string.IsNullOrWhiteSpace(Items.Name) && Items.Name != item.Name)
            {
                // throw error if the new Item name is already taken
                if (_context.Groups.Any(x => x.Name == Items.Name))
                    throw new AppException("Item Name " + Items.Name + " is already taken");

                item.Name = Items.Name;
            }

            // update store ApplicationId if it has changed
            if (Items.ApplicationId != 0 && Items.ApplicationId != item.ApplicationId)
            {
                if (!_context.Applications.Any(x => x.Id == Items.ApplicationId))
                    throw new AppException("Application Id \"" + Items.ApplicationId + "\" is not valid");

                item.ApplicationId = Items.ApplicationId;
            }

            // update store Description if it has changed
            if (!string.IsNullOrWhiteSpace(Items.Description) && Items.Description != item.Description)
            {
                item.Description = Items.Description;
            }

            // update store IsWeighable if it has changed
            if ( Items.IsWeighable != item.IsWeighable)
            {
                item.IsWeighable = Items.IsWeighable;
            }

            // update store Unit if it has changed
            if (Items.Unit != 0 && Items.Unit != item.Unit)
            {
                item.Unit = Items.Unit;
            }


            item.UpdatedBy = users.Id;
            item.UpdatedDate = DateTime.Now;

            _context.Items.Update(item);
            _context.SaveChanges();

            return item;
        }
    }
}
