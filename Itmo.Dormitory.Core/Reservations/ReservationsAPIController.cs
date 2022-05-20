using System.Threading.Tasks;
using Itmo.Dormitory.Core.Reservations.Commands;
using Itmo.Dormitory.Core.Reservations.Queries;
using MediatR;
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
        
        [HttpGet("get-available-slots/{roomName}")]
        public async Task<GetAvailableSlots.Result> GetAvailableSlots(string roomName)
        {
            return await _mediator.Send(new GetAvailableSlots.Query(roomName));
        }
        
        [HttpGet("get-list-by-owner/{isuNumber}")]
        public async Task<ActionResult> GetReservationsByOwner(string isuNumber)
        {
            await _mediator.Send(new GetReservationsByOwner.Query(isuNumber));
            return Ok();
        }
        
        [HttpPut("reserve-slot")]
        public async Task<Reserve.Result> ReserveSlot(Reserve.Command command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpPut("cancel-reservation/{id}")]
        public async Task<ActionResult> ReserveSlot(int id)
        {
            await _mediator.Send(new CancelReservation.Command(id));
            return Ok();
        }
        
        [HttpPost("create-slot")]
        public async Task<ActionResult<CreateSlot.Response>> CreateSlot(CreateSlot.Command command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpPost("remove-slot/{id}")]
        public async Task<ActionResult> RemoveSlot(int id)
        {
            await _mediator.Send(new RemoveSlot.Command(id));
            return Ok();
        }
    }
}