using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    //[Authorize(Policy = "IsAdmin")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Actions

        [ActionName("Index")]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetItemsAsync(Events.Queries.GetItemsAsync.Query query)
        {
            var model = await _mediator.Send(query);

            return new ObjectResult(model.Results);
        }

        [ActionName("Index")]
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> GetItemAsync(Events.Queries.GetItemAsync.Query query)
        {
            var model = await _mediator.Send(query);
            var result = (null != model) ? new ObjectResult(model) : new NotFoundObjectResult(query);
            return result;
        }

        [HttpPost]
        [ActionName("Create")]
        [Route("")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SetItemAsync([FromBody]Events.Commands.SetItemAsync.Command command)
        {
            var isSuccess = await _mediator.Send(command);
            return new ObjectResult(isSuccess);
        }

        [HttpPut]
        [ActionName("Update")]
        [Route("")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateItemAsync([FromBody]Events.Commands.UpdateItemAsync.Command command)
        {
            var isSuccess = await _mediator.Send(command);
            return new ObjectResult(isSuccess);
        }

        [HttpDelete]
        [ActionName("Delete")]
        [Route("")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveItemAsync([FromBody]Events.Commands.RemoveItemAsync.Command command)
        {
            var isSuccess = await _mediator.Send(command);
            return new ObjectResult(isSuccess);
        }

        #endregion
        
    }
}
