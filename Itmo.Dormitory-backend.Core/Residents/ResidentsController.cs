using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory_backend.Core.Residents.Commands;
using Itmo.Dormitory_backend.Core.Residents.Queries;

namespace Itmo.Dormitory_backend.Core.Residents
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class ResidentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResidentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<CreateResident.Response>> CreateTeacher(CreateResident.Command command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpPost("edit")]
        public async Task<ActionResult<EditResident.Response>> CreateTeacherById(EditResident.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteTeacherById(DeleteResident.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetResidentById.Response>> GetTeacherById(GetResidentById.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}