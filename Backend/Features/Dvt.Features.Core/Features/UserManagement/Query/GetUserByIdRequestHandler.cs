using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dvt.Features.Core.Features.UserManagement.Query
{
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdQueryRequest, OperationResult<GetUserByIdQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public GetUserByIdRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OperationResult<GetUserByIdQueryResponse>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetUserByIdQueryResponse(request.MessageId);
            OperationResult<GetUserByIdQueryResponse> result;

            var user = await _dbContext.UserAccount.SingleOrDefaultAsync(u => u.UserAccountId == null, cancellationToken: cancellationToken);

            if (user.NotNull())
            {
                response.Result = new UserResponse
                {
                    //Id = user.UserAccountId,
                    //FirstName = user.FirstName,
                    //Surname = user.Surname,
                    //Mobile = user.MobileNumber,
                    //EmailAddress = user.EmailAddress,
                    //IdNumber = user.IdNumber,
                    //CompanyId = user.CompanyId
                  
                };

                result = new OperationResult<GetUserByIdQueryResponse>(EnumOperationResult.Ok, null, response);
            }
            else
            {
                result = new OperationResult<GetUserByIdQueryResponse>(EnumOperationResult.None);
            }

            return result;

        }
    }
}
