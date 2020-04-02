using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class CityMasters
    {
        public CityMasters()
        {
            Stores = new HashSet<Stores>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stores> Stores { get; set; }
    }
}
