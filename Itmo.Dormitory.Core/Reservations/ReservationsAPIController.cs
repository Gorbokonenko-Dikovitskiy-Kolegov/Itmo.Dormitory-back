using System;
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
        
        [HttpGet("get-list/{roomName}")]
        public async Task<GetAvailableSlots.Result> GetAvailableSlots(string roomName)
        {
            return await _mediator.Send(new GetAvailableSlots.Query(roomName));
        }
        
        [HttpPut("reserve-slot/{id}")]
        public async Task<bool> ReserveSlot(int id)
        {
            return await _mediator.Send(new Reserve.Command(id));
        }
        
        [HttpPost("create-slot/{roomName}/{starts}")]
        public async Task CreateSlot(string roomName, DateTime starts)
        {
            await _mediator.Send(new Create.Command(roomName, starts));
        }
        
        [HttpPost("remove-slot/{id}")]
        public async Task RemoveSlot(int id)
        {
            await _mediator.Send(new Remove.Command(id));
        }
    }
}