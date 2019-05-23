using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Features.Messages.Request;
using Dvt.Features.Messages.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace wtw.webapp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(IConfiguration configuration, IMediator mediator) : base(configuration, mediator) { }


        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            if (request.IsNull()) return ModelNullError();
            var identityClaims = (ClaimsIdentity)User.Identity;
            var model = new UserAddCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }


        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var request = new GetAllUsersRequest();
            
            var model = new GetAllUsersQueryRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        [HttpGet]
        [Route("GetUserById")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var request = new GetUserByIdRequest
            {
                Id = Id
            };

            var model = new GetUserByIdQueryRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        [HttpPost]
        [Route("DisableUser")]
        public async Task<IActionResult> DisableUser(Guid userAccountId)
        {
            var request = new DisableUserRequest
            {
                userAccountId = userAccountId
            };

            var model = new DisableUserCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            var model = new UserUpdateCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        [HttpPut]
        [Route("AddPassword")]
        public async Task<IActionResult> AddPassword(AddPasswordRequest request)
        {
            var model = new AddPasswordCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }
    }
}
