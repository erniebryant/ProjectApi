using AutoMapper;
using MediatR;
using ProjectApi.Domain;
using ProjectApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectApi.Events.Commands.SetItemAsync
{
    public class SetItemAsyncHandler : IRequestHandler<Command, ViewModel>
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public SetItemAsyncHandler(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public async Task<ViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var cmd = _mapper.Map<Command, Item>(request);
            Item result = await _itemService.Create(cmd);
            return new ViewModel { Result = _mapper.Map<Item, ViewModels.Item>(result) };
        }
    }
}
