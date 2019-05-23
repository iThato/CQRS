using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Events;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Helpers;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dvt.Features.Core.Features.UserManagement.Query
{
    public class UserLoginQueryRequestHandler : IRequestHandler<UserLoginQueryRequest, OperationResult<UserLoginQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public UserLoginQueryRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<UserLoginQueryResponse>> Handle(UserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new UserLoginQueryResponse(request.MessageId); 
            OperationResult<UserLoginQueryResponse> result;

            var user = await _dbContext.UserAccount.Include(f=>f.SystemProfile).ThenInclude(g=>g.SystemProfileFunction).SingleOrDefaultAsync(u => u.Username == request.TransferObject.Username, cancellationToken: cancellationToken);
            if (user.NotNull() && user.Password.NotNull())
            {
                var hashedPassword = PasswordHelper.GenerateSaltedHash(request.TransferObject.Password, user.Salt);

                if (user.Password == hashedPassword)
                {
                    response.Result.Email = user.Email;
                    response.Result.UserAccountId = user.UserAccountId;
                    response.Result.SystemFunctions = user.SystemProfile.SystemProfileFunction.Select(f => f.SystemFunctionId).ToList();

                    result = new OperationResult<UserLoginQueryResponse>(EnumOperationResult.Ok, null, response);
                    result.AddDomainEvent(new UserLoginEvent
                    {
                        FirstName = response.Result.FirstName

                    });
                }
                else
                {
                    result = new OperationResult<UserLoginQueryResponse>(EnumOperationResult.Forbidden);
                }
            }
            else
            {
                result = new OperationResult<UserLoginQueryResponse>(EnumOperationResult.NotFound);
            }

            return result;
        }
    }
}
