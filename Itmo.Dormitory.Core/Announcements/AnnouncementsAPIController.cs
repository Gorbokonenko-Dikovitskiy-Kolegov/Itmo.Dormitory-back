using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.Core.Announcements.Commands;
using Itmo.Dormitory.Core.Announcements.Queries;
using System;
using Microsoft.AspNetCore.Http;

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
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public async Task<ActionResult<CreateAnnouncement.Response>> CreateAnnouncement(CreateAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Изменить существующее объявление
        /// </summary>
        /// <response code="200">Returns edited item</response>
        /// <response code="422">Entity not found</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("edit")]
        public async Task<ActionResult<EditAnnouncement.Response>> EditAnnouncement(EditAnnouncement.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить существующее объявление
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="422">Entity not found</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAnnouncementById(DeleteAnnouncement.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Получить существующее объявление
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="422">Entity not found</response>
        /// <response code="400">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<GetAnnouncementById.Response>> GetAnnouncementById(Guid id)
        {
            return await _mediator.Send(new GetAnnouncementById.Query(id));
        }

        /// <summary>
        /// Получить все существующие объявления
        /// </summary>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("get-list")]
        public async Task<ActionResult<GetAnnouncementsList.Response>> GetAnnouncementsList()
        {
            return await _mediator.Send(new GetAnnouncementsList.Query());
        }
    }
}
