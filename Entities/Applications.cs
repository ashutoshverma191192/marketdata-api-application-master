using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class Applications
    {
        public Applications()
        {
            Groups = new HashSet<Groups>();
            Items = new HashSet<Items>();
            Qualities = new HashSet<Qualities>();
            Stores = new HashSet<Stores>();
            SubGroups = new HashSet<SubGroups>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double BillingAmount { get; set; }
        public int BillingFrequency { get; set; }
        public string BilledBy { get; set; }
        public DateTime? LastBillPaidOn { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<Items> Items { get; set; }
        public virtual ICollection<Qualities> Qualities { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
        public virtual ICollection<SubGroups> SubGroups { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
