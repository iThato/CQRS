using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.SystemFunctionManagement.Messages;
using Dvt.Features.Messages.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using wtw.webapp.Controllers;


namespace dvt.webapp.Controllers
{
    /// <summary>
    /// API Controller for System Functions
    /// </summary>
    [ApiController]
    [AllowAnonymous] //For now
    [Route("api/[controller]")]
    public class SystemFunctionController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mediator"></param>
        public SystemFunctionController(IConfiguration configuration, IMediator mediator) : base(configuration, mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSystemFunctionGroup")]
        public async Task<IActionResult> AddSystemFunctionGroup([FromBody] AddSystemGroupRequest request)
        {
            if (request.IsNull()) return ModelNullError();
            var identityClaims = (ClaimsIdentity)User.Identity;
            
            var model = new FnGroupAddCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSystemFunctionGroups")]
        public async Task<IActionResult> GetSystemFunctionGroups()
        {
            var request = new GetAllSystemGroupsRequest();
            var model = new GetAllSysFunctionGroupsQueryRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">Add System Function Request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSystemFunction")]
        public async Task<IActionResult> AddSystemFunction([FromBody] AddSystemFunctionRequest request)
        {
            if (request.IsNull()) return ModelNullError();
            var identityClaims = (ClaimsIdentity)User.Identity;

            var model = new SysFunctionAddCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">Link System Function to Profile</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSystemProfileFunctionLink")]
        public async Task<IActionResult> AddSystemProfileFunctionLink([FromBody] LinkSystemProfileFunctionRequest request)
        {
            if (request.IsNull()) return ModelNullError();
            var identityClaims = (ClaimsIdentity)User.Identity;

            var model = new SysProfileFunctionLinkCommandRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }
    }
}
