using Itmo.Dormitory.Core.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Users
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersAPIController
    {
        private readonly IMediator _mediator;

        public UsersAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создать пользователя сайта
        /// </summary>
        [HttpPost("create-user")]
        public async Task<ActionResult<CreateUser.Response>> CreateUser(CreateUser.Command command)
        {
            return await _mediator.Send(command);
        }
        /// <summary>
        /// Создать управляющего
        /// </summary>
        [HttpPost("create-admin")]
        public async Task<ActionResult<CreateAdmin.Response>> CreateAdmin(CreateAdmin.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}
