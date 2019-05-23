using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.Course.Messages;
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
    public class CourseController : BaseController
    {
        public CourseController(IConfiguration configuration, IMediator mediator) : base(configuration, mediator) { }



        [HttpGet]
        [Route("GetAllCourse")]
        [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCourse()
        {
            var request = new GetAllCourseRequest();

            var model = new GetAllCoursesQueryRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);

        }
        
        [HttpGet]
        [Route("GetCourseById")]
        [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById(int Id)
        {
            var request = new GetCourseByIdRequest
            {
                Id = Id
            };

            var model = new GetCourseByIdQueryRequest
            {
                TransferObject = request
            };
            var result = await Mediator.Send(model);

            return HandleResponse(result, r => r);
        }

    }
}
