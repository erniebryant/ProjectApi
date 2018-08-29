using System;
using System.Collections.Generic;
using System.Linq;
using ProjectApi.Data.Models;

namespace ProjectApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(APIContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Items.Any())
                return;

            Tag tag1 = new Tag() { Id = 1, Name = "MyTag", slug = "my-tag", LastUpdated = DateTime.Now };
            Tag tag2 = new Tag() { Id = 2, Name = "MyTag 2", slug = "my-tag-2", LastUpdated = DateTime.Now };
            List<Tag> tags = new List<Tag>();
            tags.Add(tag1);
            tags.Add(tag2);

            var item = new Item() { Description = "Initial Item 1", Name = "Demo 1", LastUpdated = DateTime.Now, Tags = tags};
            var item2 = new Item() { Description = "Initial Item 2", Name = "Demo 2", LastUpdated = DateTime.Now, Tags = tags };
            context.Add(item);
            context.Add(item2);

            context.SaveChanges();
            return;
        }
    
        
    }
}