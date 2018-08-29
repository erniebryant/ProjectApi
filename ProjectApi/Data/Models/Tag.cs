using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string slug { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
