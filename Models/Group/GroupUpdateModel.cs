using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Group
{
    public class GroupUpdateModel
    {
        public string Name { get; set; }
        public int ApplicationId { get; set; }
    }
}
