using Itmo.Dormitory.Core.Applications.Commands;
using Itmo.Dormitory.Core.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Applications
{
    [ApiController]
    [Route("api/v1/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateApplication.Response>> CreateAnnouncement(CreateApplication.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<EditApplication.Response>> EditAnnouncement(EditApplication.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteAnnouncementById(DeleteApplication.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetApplicationById.Response>> GetAnnouncementById(GetApplicationById.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("get-by-resident")]
        public async Task<ActionResult<GetApplicationsByResident.Response>> GetTeachersList(GetApplicationsByResident.Query query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost("get-list")]
        public async Task<ActionResult<GetApplicationsList.Response>> GetTeachersList(GetApplicationsList.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}
