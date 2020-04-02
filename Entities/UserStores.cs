using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public partial class UserStores
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
    }
}
