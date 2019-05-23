using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Dvt.Features.Core.Features.Course.Messages;

namespace Dvt.Features.Core.Features.Course.Query
{
    public class GetAllCoursesRequestHandler : IRequestHandler<GetAllCoursesQueryRequest, OperationResult<GetAllCoursesQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public GetAllCoursesRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<GetAllCoursesQueryResponse>> Handle(GetAllCoursesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllCoursesQueryResponse(request.MessageId);
            OperationResult<GetAllCoursesQueryResponse> result;

            response.Result = await (from u in _dbContext.Course
                                     select new CourseResponse
                                     {
                                         Id = u.Id,
                                         Code = u.Code,
                                         Name = u.Name,
                                         Description = u.Description

                                     }).ToListAsync();

            result = new OperationResult<GetAllCoursesQueryResponse>(EnumOperationResult.Ok, null, response);

            return result;
        }
    }
}

