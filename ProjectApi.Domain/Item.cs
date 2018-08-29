using System;
using System.Collections.Generic;

namespace ProjectApi.Domain
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
