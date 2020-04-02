using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class Items
    {
        public Items()
        {
            Qualities = new HashSet<Qualities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }
        public bool IsWeighable { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual Applications Application { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users UpdatedByNavigation { get; set; }
        public virtual ICollection<Qualities> Qualities { get; set; }
    }
}
