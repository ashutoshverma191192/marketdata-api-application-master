using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Qualities
{
    public class QualitiesUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public int ApplicationId { get; set; }
    }
}
