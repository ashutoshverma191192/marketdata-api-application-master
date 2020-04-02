using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Stores
{
    public class RegisterStoreModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PhoneNumber2 { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        public string Remark { get; set; }

    }
}
