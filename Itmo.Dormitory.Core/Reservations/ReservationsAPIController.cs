using System.Threading.Tasks;
using Itmo.Dormitory.Core.Reservations.Commands;
using Itmo.Dormitory.Core.Reservations.Queries;
using Itmo.Dormitory.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Core.Reservations
{
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationsAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Получить доступные слоты записи
        /// </summary>
        /// <response code="400">Validation error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("create")]
        [HttpGet("get-available-slots/{roomName}/{page}")]
        public async Task<PagedResponse<Reservation>> GetAvailableSlots(string roomName, int page)
        {
            return await _mediator.Send(new GetAvailableSlots.Query(roomName, page));
        }


        /// <summary>
        /// Получить слоты записи, забронированные данным пользователем
        /// </summary>
        /// <response code="400">Validation error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("get-list-by-owner/{isuNumber}/{page}")]
        public async Task<PagedResponse<Reservation>> GetReservationsByOwner(string isuNumber, int page)
        {
            return await _mediator.Send(new GetReservationsByOwner.Query(isuNumber, page));
        }

        /// <summary>
        /// Забронировать слот
        /// </summary>
        /// <response code="400">Validation error</response>
        [HttpPut("reserve-slot")]
        public async Task<ReserveSlot.Result> ReserveSlot(ReserveSlot.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Отменить бронь
        /// </summary>
        /// <response code="400">Validation error</response>
        [HttpPut("cancel-reservation/{id}")]
        public async Task<ActionResult> CancelReservation(int id)
        {
            await _mediator.Send(new CancelReservation.Command(id));
            return Ok();
        }
        /// <summary>
        /// Создать слот записи
        /// </summary>
        /// <response code="400">Validation error</response>
        [HttpPost("create-slot")]
        public async Task<CreateSlot.Response> CreateSlot(CreateSlot.Command command)
        {
            return await _mediator.Send(command);
        }
        /// <summary>
        /// Удалить слот записи
        /// </summary>
        /// <response code="400">Validation error</response>
        [HttpPost("remove-slot/{id}")]
        public async Task<ActionResult> RemoveSlot(int id)
        {
            await _mediator.Send(new RemoveSlot.Command(id));
            return Ok();
        }
    }
}