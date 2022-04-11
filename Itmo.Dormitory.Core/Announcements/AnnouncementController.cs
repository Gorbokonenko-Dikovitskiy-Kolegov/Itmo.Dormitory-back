using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.Core.Announcements.Commands;
using Itmo.Dormitory.Core.Announcements.Queries;

namespace Itmo.Dormitory.Core.Announcements
{
    [ApiController]
    [Route("api/v1/announcements")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnnouncementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateAnnouncement.Response>> CreateAnnouncement(CreateAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<EditAnnouncement.Response>> EditAnnouncement(EditAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteAnnouncementById(DeleteAnnouncement.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetAnnouncementById.Response>> GetAnnouncementById(GetAnnouncementById.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("get-list")]
        public async Task<ActionResult<GetAnnouncementsList.Response>> GetTeachersList(GetAnnouncementsList.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}
