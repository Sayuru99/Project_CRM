using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.SharedDomain.Exceptions;
using System.Net;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Models.User;

namespace MyApp.Application.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : MediatRControllerBase<
        UserModel,
        GetUsersPaginateQuery,
        GetUsersPaginateResponse,
        GetUserQuery,
        GetUserResponse,
        InsertUserCommand,
        UpdateUserCommand,
        DeleteUserCommand>
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public override Task<IActionResult> Insert([FromForm] InsertUserCommand request)
        {
            return base.Insert(request);
        }

        [HttpGet("{param}")]
        public async override Task<IActionResult> Get(string param)
        {
            var request = new GetUserQuery();

            if (Guid.TryParse(param, out var _)) request.Id = new Guid(param);
            else request.Email = param;
  
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpPatch("inactive")]
        [Authorize]
        public async Task<IActionResult> InactiveUser(InactiveUserCommand request)
        {
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpPatch("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdateUserPassword request)
        {
            return await Result(request, HttpStatusCode.OK);
        }
    }
}
