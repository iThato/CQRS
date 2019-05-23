using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.Course.Messages;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dvt.Features.Core.Features.Course.Query
{
    public class GetCourseByIdRequestHandler : IRequestHandler<GetCourseByIdQueryRequest, OperationResult<GetCourseByIdQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public GetCourseByIdRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OperationResult<GetCourseByIdQueryResponse>> Handle(GetCourseByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetCourseByIdQueryResponse(request.MessageId);
            OperationResult<GetCourseByIdQueryResponse> result;

            var user = await _dbContext.Course.SingleOrDefaultAsync(u => u.Code == null, cancellationToken: cancellationToken);

            if (user.NotNull())
            {
                response.Result = new CourseResponse
                {
                    //Id = user.UserAccountId,
                    //FirstName = user.FirstName,
                    //Surname = user.Surname,
                    //Mobile = user.MobileNumber,
                    //EmailAddress = user.EmailAddress,
                    //IdNumber = user.IdNumber,
                    //CompanyId = user.CompanyId

                };

                result = new OperationResult<GetCourseByIdQueryResponse>(EnumOperationResult.Ok, null, response);
            }
            else
            {
                result = new OperationResult<GetCourseByIdQueryResponse>(EnumOperationResult.None);
            }

            return result;

        }
    }
}
