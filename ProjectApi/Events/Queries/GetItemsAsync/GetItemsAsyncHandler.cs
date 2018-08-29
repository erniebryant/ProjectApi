using AutoMapper;
using MediatR;
using ProjectApi.Domain;
using ProjectApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectApi.Events.Queries.GetItemsAsync
{
    public class GetItemsAsyncHandler : IRequestHandler<Query, ViewModel>
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public GetItemsAsyncHandler(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }
        public async Task<ViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get items from AdminApi
            var items = await _itemService.GetAll();

            if (items == null) return null;

            // Map tp ViewModel object
            var results = _mapper.Map<IEnumerable<Item>, List<ViewModels.Item>>(items);

            // return ViewModel
            var viewModel = new ViewModel { Results = results.AsQueryable() };
            return viewModel;
        }
    }
}
