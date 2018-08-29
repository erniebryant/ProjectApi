using System;

namespace ProjectApi.ViewModels
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string slug { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
