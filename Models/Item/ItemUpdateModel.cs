using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Item
{
    public class ItemUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }
        public bool IsWeighable { get; set; }
        public int ApplicationId { get; set; }
    }
}
