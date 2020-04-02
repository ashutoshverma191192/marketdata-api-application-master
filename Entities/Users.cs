using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class Users
    {
        public Users()
        {
            GroupsCreatedByNavigation = new HashSet<Groups>();
            GroupsUpdatedByNavigation = new HashSet<Groups>();
            ItemsCreatedByNavigation = new HashSet<Items>();
            ItemsUpdatedByNavigation = new HashSet<Items>();
            Qualities = new HashSet<Qualities>();
            SubGroupsCreatedByNavigation = new HashSet<SubGroups>();
            SubGroupsUpdatedByNavigation = new HashSet<SubGroups>();
            UserRoles = new HashSet<UserRoles>();
            UserStores = new HashSet<UserStores>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
        public bool IsLocked { get; set; }
        public int StoreId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Applications Application { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<Groups> GroupsCreatedByNavigation { get; set; }
        public virtual ICollection<Groups> GroupsUpdatedByNavigation { get; set; }
        public virtual ICollection<Items> ItemsCreatedByNavigation { get; set; }
        public virtual ICollection<Items> ItemsUpdatedByNavigation { get; set; }
        public virtual ICollection<Qualities> Qualities { get; set; }
        public virtual ICollection<SubGroups> SubGroupsCreatedByNavigation { get; set; }
        public virtual ICollection<SubGroups> SubGroupsUpdatedByNavigation { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<UserStores> UserStores { get; set; }
    }
}
