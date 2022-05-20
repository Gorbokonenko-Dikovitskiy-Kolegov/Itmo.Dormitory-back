using Itmo.Dormitory.Core.Applications.Commands;
using Itmo.Dormitory.Core.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Applications
{
    [ApiController]
    [Route("api/v1/applications")]
    public class ApplicationsAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Создать новое заявление 
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreateApplication.Response>> CreateApplication(CreateApplication.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Изменить существующее заявление
        /// </summary>
        [HttpPut("edit")]
        public async Task<ActionResult<EditApplication.Response>> EditApplication(EditApplication.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить существующее заявление
        /// </summary>
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteApplicationById(DeleteApplication.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Получить существующее заявление
        /// </summary>
        [HttpGet("get-by-id")]
        public async Task<ActionResult<GetApplicationById.Response>> GetApplicationById(Guid id)
        {
            return await _mediator.Send(new GetApplicationById.Query(id));
        }

        /// <summary>
        /// Получить все заявления поданые данным пользователем
        /// </summary>
        [HttpGet("get-by-resident")]
        public async Task<ActionResult<GetApplicationsByResident.Response>> GetApplicationsByResident(Guid id)
        {
            return await _mediator.Send(new GetApplicationsByResident.Query(id));
        }

        /// <summary>
        /// Получить все существующие заявления
        /// </summary>
        [HttpGet("get-list")]
        public async Task<ActionResult<GetApplicationsList.Response>> GetApplicationsList()
        {
            return await _mediator.Send(new GetApplicationsList.Query());
        }
    }
}
