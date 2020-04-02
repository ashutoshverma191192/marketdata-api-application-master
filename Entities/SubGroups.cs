using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class SubGroups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? City { get; set; }
        public int? PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public double? OpeningBalance { get; set; }
        public bool ShowInSaleBill { get; set; }
        public bool IsExpense { get; set; }
        public int GroupId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual Applications Application { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Groups Group { get; set; }
        public virtual Users UpdatedByNavigation { get; set; }
    }
}
