using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.Core.Announcements.Commands;
using Itmo.Dormitory.Core.Announcements.Queries;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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

        /// <summary>
        /// Создать новое объявление
        /// </summary>
        /// <response code="200">Returns the newly created item</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("create")]
        public async Task<ActionResult<CreateAnnouncement.Response>> CreateAnnouncement(CreateAnnouncement.Command command)
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();
            if (User != null && !User.IsInRole("Admin"))
                return NotFound();

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Изменить существующее объявление
        /// </summary>
        /// <response code="200">Returns edited item</response>
        /// <response code="422">Entity not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("edit")]
        public async Task<ActionResult<EditAnnouncement.Response>> EditAnnouncement(EditAnnouncement.Command command)
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();
            if (User != null && !User.IsInRole("Admin"))
                return NotFound();

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить существующее объявление
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="422">Entity not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAnnouncementById(DeleteAnnouncement.Command command)
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();
            if (User != null && !User.IsInRole("Admin"))
                return NotFound();

            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Получить существующее объявление
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="422">Entity not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<GetAnnouncementById.Response>> GetAnnouncementById(Guid id)
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();

            return await _mediator.Send(new GetAnnouncementById.Query(id));
        }

        /// <summary>
        /// Получить все существующие объявления
        /// </summary>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("get-list")]
        public async Task<ActionResult<GetAnnouncementsList.Response>> GetAnnouncementsList()
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();

            return await _mediator.Send(new GetAnnouncementsList.Query());
        }
    }
}
