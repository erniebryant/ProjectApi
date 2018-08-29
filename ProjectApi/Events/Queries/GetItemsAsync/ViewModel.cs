using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Events.Queries.GetItemsAsync
{
    public class ViewModel
    {
        public IEnumerable<ViewModels.Item> Results { get; set; }
    }
}
