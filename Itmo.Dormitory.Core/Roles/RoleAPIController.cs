using System.Threading.Tasks;
using Itmo.Dormitory.Core.Roles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Core.Roles
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RoleAPIController
    {

        private readonly IMediator _mediator;

        public RoleAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }
   
        [HttpPost("create-role")]
        public async Task<ActionResult<CreateRole.Response>> CreateRole(CreateRole.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}
