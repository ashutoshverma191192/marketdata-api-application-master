using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.SubGroups
{
    public class RegisterSubGroupModel
    {
        [Required]
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? City { get; set; }
        public int? PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public double? OpeningBalance { get; set; }
        [Required]
        public bool ShowInSaleBill { get; set; }
        [Required]
        public bool IsExpense { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
