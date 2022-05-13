using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.Core.Announcements.Commands;
using Itmo.Dormitory.Core.Announcements.Queries;
using System;

namespace Itmo.Dormitory.Core.Announcements
{
    [ApiController]
    [Route("api/v1/announcements")]
    public class AnnouncementsAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementsAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateAnnouncement.Response>> CreateAnnouncement(CreateAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<EditAnnouncement.Response>> EditAnnouncement(EditAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAnnouncementById(DeleteAnnouncement.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<GetAnnouncementById.Response>> GetAnnouncementById(Guid id)
        {
            return await _mediator.Send(new GetAnnouncementById.Query(id));
        }

        [HttpGet("get-list")]
        public async Task<ActionResult<GetAnnouncementsList.Response>> GetAnnouncementsList()
        {
            return await _mediator.Send(new GetAnnouncementsList.Query());
        }
    }
}
