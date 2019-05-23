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

namespace Dvt.Features.Core.Features.UserManagement.Query
{
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersQueryRequest, OperationResult<GetAllUsersQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public GetAllUsersRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllUsersQueryResponse(request.MessageId);
            OperationResult<GetAllUsersQueryResponse> result;

            response.Result = await (from u in _dbContext.UserAccount
                                     select new UserResponse
                                     {
                                         UserAccountId = u.UserAccountId,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Email = u.Email,
                                         ContactNumber = u.ContactNumber,
                                         SystemProfileId = u.SystemProfileId,
                                         Username = u.Username,
                                         AcceptedTerms = u.AcceptedTerms,
                                         KnownAs = u.KnownAs,
                                         SystemProfile = u.SystemProfile.DisplayName
                                        
                                     }).ToListAsync();

            result = new OperationResult<GetAllUsersQueryResponse>(EnumOperationResult.Ok, null, response);

            return result;
        }
    }
}
