using FluentValidation;
using Itmo.Dormitory.Core.Activities.Commands;
using Itmo.Dormitory.Core.Activities.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Activities
{
    [ApiController]
    [Route("api/v1/activity")]
    public class ActivitiesAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("cool-feature-for-Makarevich")]
        public async Task<ActionResult<GetActivities.Response>> CoolFeatureForMakarevich()
        {
            if (User != null && !User.Identity.IsAuthenticated)
                return Unauthorized();

            return await _mediator.Send(new GetActivities.Query());
        }

        [HttpGet, Route("refresh")]
        public async Task<ActionResult> Refresh()
        {
            await _mediator.Send(new Refresh.Command());
            return Ok();
        }
    }
}
