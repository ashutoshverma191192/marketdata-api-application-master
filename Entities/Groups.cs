using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class Groups
    {
        public Groups()
        {
            SubGroups = new HashSet<SubGroups>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual Applications Application { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users UpdatedByNavigation { get; set; }
        public virtual ICollection<SubGroups> SubGroups { get; set; }
    }
}
