using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class Stores
    {
        public Stores()
        {
            UserStores = new HashSet<UserStores>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityId { get; set; }
        public int PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string ContactPerson { get; set; }
        public string Remark { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Applications Application { get; set; }
        public virtual CityMasters City { get; set; }
        public virtual ICollection<UserStores> UserStores { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
