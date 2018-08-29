using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Events.Commands.RemoveItemAsync
{
    public class Command : IRequest<ViewModel>
    {
        public long Id { get; set; }
    }
}
