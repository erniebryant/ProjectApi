using System;

namespace ProjectApi.Domain
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string slug { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
