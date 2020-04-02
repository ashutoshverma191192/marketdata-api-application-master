using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IGroupService
    {
        IEnumerable<Groups> GetAll();
        Groups GetById(int id);
        Groups Create(Groups groups, Users users);
        Groups Update(Groups groups,Users users);
        void Delete(int id);
    }
    public class GroupService : IGroupService
    {
        private DataContext _context;

        public GroupService(DataContext context)
        {
            _context = context;
        }
        public Groups Create(Groups groups, Users users)
        {
            // validation
            if (_context.Groups.Any(x => x.Name == groups.Name))
                throw new AppException("Group Name \"" + groups.Name + "\" is already taken");

            groups.ApplicationId = users.ApplicationId;
            groups.CreatedBy = users.Id;
            groups.CreatedDate = DateTime.Now;
            groups.UpdatedBy = users.Id;
            groups.UpdatedDate = DateTime.Now;

            _context.Groups.Add(groups);
            _context.SaveChanges();

            return groups;
        }

        public void Delete(int id)
        {
            var group = _context.Groups.Find(id);

            if (group == null)
                throw new AppException("Group not found");

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public IEnumerable<Groups> GetAll()
        {
            return _context.Groups;
        }

        public Groups GetById(int id)
        {
            var group = _context.Groups.Find(id);

            if (group == null)
                throw new AppException("Group not found");

            return group;
        }

        public Groups Update(Groups groups, Users users)
        {
            var group = _context.Groups.Find(groups.Id);

            if (group == null)
                throw new AppException("Group not found");

            // update group name if it has changed
            if (!string.IsNullOrWhiteSpace(groups.Name) && groups.Name != group.Name)
            {
                // throw error if the new group name is already taken
                if (_context.Groups.Any(x => x.Name == groups.Name))
                    throw new AppException("Group Name " + groups.Name + " is already taken");

                group.Name = groups.Name;
            }
            // update store AddressLine2 if it has changed
            if (groups.ApplicationId != 0 && groups.ApplicationId != group.ApplicationId)
            {
                if (!_context.Applications.Any(x => x.Id == groups.ApplicationId))
                    throw new AppException("Application Id \"" + groups.ApplicationId + "\" is not valid");

                group.ApplicationId = groups.ApplicationId;
            }

            group.UpdatedBy = users.Id;
            group.UpdatedDate = DateTime.Now;

            _context.Groups.Update(group);
            _context.SaveChanges();

            return group;
        }
    }
}
