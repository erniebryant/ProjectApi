using AutoMapper;
using MediatR;
using ProjectApi.Domain;
using ProjectApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectApi.Events.Queries.GetItemAsync
{
    public class GetItemAsyncHandler : IRequestHandler<Query, ViewModel>
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public GetItemAsyncHandler(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public async Task<ViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get items from AdminApi
            var items = await _itemService.Get(request.Id);

            if (items == null) return null;

            // Project to mapped ViewModel object
            var results = _mapper.Map<Item, ViewModels.Item>(items);

            // return ViewModel
            var viewModel = new ViewModel { Results = results };
            return viewModel;
        }
    }
}
