using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace wtw.webapp.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private static readonly Lazy<IEnumerable<ValidationError>> _modelNullErrorList =
            new Lazy<IEnumerable<ValidationError>>(() => new List<ValidationError> {new ValidationError("Model may not be null")});

        protected readonly IConfiguration Configuration;

        public BaseController(IConfiguration configuration, IMediator mediator)
        {
            Configuration = configuration;
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        protected IActionResult HandleResponse<T>(OperationResult<T> result, Func<CreatedAtRouteResult> createdAt = null)
        {
            switch (result.Status)
            {
                case EnumOperationResult.Ok:
                    return Ok(result.Entity);
                case EnumOperationResult.Added:
                    if (createdAt.NotNull())
                        return createdAt();
                    return Created("", result.Entity);
                case EnumOperationResult.Updated:
                    return Ok(result.Entity);
                case EnumOperationResult.Accepted:
                    return Accepted(result.Entity);
                case EnumOperationResult.NotFound:
                    return NotFound();
                case EnumOperationResult.Duplicate:
                    return StatusCode(StatusCodes.Status409Conflict, result.Entity);
                case EnumOperationResult.Error:
                    return BadRequest(result.Errors);
            }

            return Unauthorized();
        }

        protected IActionResult HandleResponse<TI, TO>(OperationResult<TI> result, Func<TI, TO> resultProjector, Func<CreatedAtRouteResult> createdAt = null)
            where TI : HandlerResponseBase
        {
            switch (result.Status)
            {
                case EnumOperationResult.Ok:
                    return Ok(resultProjector(result.Entity));
                case EnumOperationResult.Added:
                    if (createdAt.NotNull())
                        return createdAt();
                    return Created("", resultProjector(result.Entity));
                case EnumOperationResult.Updated:
                    return Ok(resultProjector(result.Entity));
                case EnumOperationResult.Accepted:
                    return Accepted(resultProjector(result.Entity));
                case EnumOperationResult.NotFound:
                    return NotFound();
                case EnumOperationResult.Duplicate:
                    if (result.Errors.Any())
                        return StatusCode(StatusCodes.Status409Conflict, result.Errors);
                    return StatusCode(StatusCodes.Status409Conflict);
                case EnumOperationResult.Error:
                    return BadRequest(result.Errors);
            }

            return Unauthorized();
        }

        protected virtual IActionResult ModelNullError()
        {
            return BadRequest(_modelNullErrorList.Value);
        }

        protected virtual async Task<IActionResult> ModelNullErrorTask()
        {
            return await Task.FromResult(BadRequest(_modelNullErrorList.Value));
        }
    }
}
