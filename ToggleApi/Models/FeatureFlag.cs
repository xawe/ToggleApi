using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToogleApi.Models
{
    public class FeatureFlag
    {
        public string Id{ get; set; }

        public string Flag { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Grupo { get; set; }

    }
}
