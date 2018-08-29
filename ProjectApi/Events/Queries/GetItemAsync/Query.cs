using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Events.Queries.GetItemAsync
{
    public class Query : IRequest<ViewModel>
    {
        public long Id { get; set; }
    }
}
