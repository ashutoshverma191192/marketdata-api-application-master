using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ISubGroupsService
    {
        IEnumerable<SubGroups> GetAll();
        SubGroups GetById(int id);
        SubGroups Create(SubGroups groups, Users users);
        SubGroups Update(SubGroups groups, Users users);
        void Delete(int id);
    }
    public class SubGroupService : ISubGroupsService
    {
        DataContext _context;
        public SubGroupService(DataContext context)
        {
            _context = context;
        }

        public SubGroups Create(SubGroups subGroups, Users users)
        {
            // validation
            if (_context.SubGroups.Any(x => x.Name == subGroups.Name))
                throw new AppException("SubGroup Name \"" + subGroups.Name + "\" is already taken");
            
            if (!_context.Groups.Any(x => x.Id == subGroups.GroupId))
                throw new AppException("GroupId \"" + subGroups.GroupId + "\" not found");

            subGroups.ApplicationId = users.ApplicationId;
            subGroups.CreatedBy = users.Id;
            subGroups.CreatedDate = DateTime.Now;
            subGroups.UpdatedBy = users.Id;
            subGroups.UpdatedDate = DateTime.Now;

            _context.SubGroups.Add(subGroups);
            _context.SaveChanges();

            return subGroups;
        }

        public void Delete(int id)
        {
            var subGroups = _context.SubGroups.Find(id);

            if (subGroups == null)
                throw new AppException("SubGroup not found");

            _context.SubGroups.Remove(subGroups);
            _context.SaveChanges();
        }

        public IEnumerable<SubGroups> GetAll()
        {
            return _context.SubGroups;
        }

        public SubGroups GetById(int id)
        {
            var subGroups = _context.SubGroups.Find(id);

            if (subGroups == null)
                throw new AppException("SubGroup not found");

            return subGroups;
        }

        public SubGroups Update(SubGroups subGroups, Users users)
        {
            if (subGroups is null)
            {
                throw new ArgumentNullException(nameof(subGroups));
            }

            var subGroup = _context.SubGroups.Find(subGroups.Id);

            if (subGroup == null)
                throw new AppException("SubGroup not found");

            // update SubGroup Name if it has changed
            if (!string.IsNullOrWhiteSpace(subGroups.Name) && subGroups.Name != subGroup.Name)
            {
                // throw error if the new SubGroup name is already taken
                if (_context.Groups.Any(x => x.Name == subGroups.Name))
                    throw new AppException("SubGroup Name " + subGroups.Name + " is already taken");

                subGroup.Name = subGroups.Name;
            }

            // update store ApplicationId if it has changed
            if (subGroups.ApplicationId != 0 && subGroups.ApplicationId != subGroup.ApplicationId)
            {
                if (!_context.Applications.Any(x => x.Id == subGroups.ApplicationId))
                    throw new AppException("Application Id \"" + subGroups.ApplicationId + "\" is not valid");

                subGroup.ApplicationId = subGroups.ApplicationId;
            }

            // update store AddressLine1 if it has changed
            if (!string.IsNullOrWhiteSpace(subGroups.AddressLine1) && subGroups.AddressLine1 != subGroup.AddressLine1)
            {
                subGroup.AddressLine1 = subGroups.AddressLine1;
            }

            // update store AddressLine2 if it has changed
            if (!string.IsNullOrWhiteSpace(subGroups.AddressLine2) && subGroups.AddressLine2 != subGroup.AddressLine2)
            {
                subGroup.AddressLine2 = subGroups.AddressLine2;
            }

            // update store AccountNumber if it has changed
            if (!string.IsNullOrWhiteSpace(subGroups.AccountNumber) && subGroups.AccountNumber != subGroup.AccountNumber)
            {
                subGroup.AccountNumber = subGroups.AccountNumber;
            }

            // update store City if it has changed
            if (subGroups.City != 0 && subGroups.City != subGroup.City)
            {
                if (!_context.Applications.Any(x => x.Id == subGroups.City))
                    throw new AppException("City Id \"" + subGroups.City + "\" is not valid");

                subGroup.City = subGroups.City;
            }

            // update store GroupId if it has changed
            if (subGroups.GroupId != 0 && subGroups.GroupId != subGroup.GroupId)
            {
                if (!_context.Applications.Any(x => x.Id == subGroups.GroupId))
                    throw new AppException("Group Id \"" + subGroups.GroupId + "\" is not valid");

                subGroup.GroupId = subGroups.GroupId;
            }

            // update store IsExpense if it has changed
            if (!string.IsNullOrWhiteSpace(Convert.ToString(subGroups.IsExpense)) && subGroups.IsExpense != subGroup.IsExpense)
            {
                subGroup.IsExpense = subGroups.IsExpense;
            }

            // update store OpeningBalance if it has changed
            if (subGroups.OpeningBalance != 0 && subGroups.OpeningBalance != subGroup.OpeningBalance)
            {
                subGroup.OpeningBalance = subGroups.OpeningBalance;
            }

            // update store PhoneNumber if it has changed
            if (!string.IsNullOrWhiteSpace(subGroups.PhoneNumber) && subGroups.PhoneNumber != subGroup.PhoneNumber)
            {
                subGroup.PhoneNumber = subGroups.PhoneNumber;
            }

            // update store PostalCode if it has changed
            if (subGroups.PostalCode != 0 && subGroups.PostalCode != subGroup.PostalCode)
            {
                subGroup.PostalCode = subGroups.PostalCode;
            }

            // update store ShowInSaleBill if it has changed
            if (!string.IsNullOrWhiteSpace(Convert.ToString(subGroups.ShowInSaleBill)) && subGroups.ShowInSaleBill != subGroup.ShowInSaleBill)
            {
                subGroup.ShowInSaleBill = subGroups.ShowInSaleBill;
            }


            subGroup.UpdatedBy = users.Id;
            subGroup.UpdatedDate = DateTime.Now;

            _context.SubGroups.Update(subGroup);
            _context.SaveChanges();

            return subGroup;
        }
    }
}
