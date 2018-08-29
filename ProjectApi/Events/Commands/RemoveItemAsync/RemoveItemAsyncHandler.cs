using AutoMapper;
using MediatR;
using ProjectApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectApi.Events.Commands.RemoveItemAsync
{
    public class RemoveItemAsyncHandler : IRequestHandler<Command, ViewModel>
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public RemoveItemAsyncHandler(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public async Task<ViewModel> Handle(Command request, CancellationToken cancellationToken)
        {
            long id = request.Id;
            bool isSuccess = await _itemService.Delete(id);
            return new ViewModel { IsSuccess = isSuccess };
        }
    }
}
