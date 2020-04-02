using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.SubGroups
{
    public class subGroupUpdateModel
    {
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
    }
}
