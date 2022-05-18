using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.Core.Residents.Commands;
using Itmo.Dormitory.Core.Residents.Queries;
using System;

namespace Itmo.Dormitory.Core.Residents
{
    [ApiController]
    [Route("api/v1/residents")]
    public class ResidentsAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResidentsAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создать нового постояльца
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreateResident.Response>> CreateResident(CreateResident.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Изменить существующего постояльца
        /// </summary>
        [HttpPut("edit")]
        public async Task<ActionResult<EditResident.Response>> EditResident(EditResident.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить существующего постояльца
        /// </summary>
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteResidentById(DeleteResident.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Получить существующего постояльца
        /// </summary>
        [HttpGet("get-by-id")]
        public async Task<ActionResult<GetResidentById.Response>> GetResidentById(Guid id)
        {
            return await _mediator.Send(new GetResidentById.Query(id));
        }
    }
}