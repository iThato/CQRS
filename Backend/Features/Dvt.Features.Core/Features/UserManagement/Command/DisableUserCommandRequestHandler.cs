using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Structures;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Messages.Enums;
using UserStatus = Dvt.Features.Messages.Enums.UserStatus;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class DisableUserCommandRequestHandler : IRequestHandler<DisableUserCommandRequest, OperationResult<DisableUserCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public DisableUserCommandRequestHandler(DvtDatabaseContext dbContex)
        {
            _dbContext = dbContex;
        }

        public async Task<OperationResult<DisableUserCommandResponse>> Handle(DisableUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new DisableUserCommandResponse(request.MessageId);

            OperationResult<DisableUserCommandResponse> result;

            var userAccount = await _dbContext.UserAccount.FindAsync(request.TransferObject.userAccountId);

            if (userAccount.NotNull())
            {
                userAccount.UserStatusId = (int)UserStatus.Disabled;

                _dbContext.UserAccount.Update(userAccount);

                await _dbContext.SaveChangesAsync();

                response.Result = true;
                result = new OperationResult<DisableUserCommandResponse>(EnumOperationResult.Ok, null, response);
            }
            else
            {
                result = new OperationResult<DisableUserCommandResponse>(EnumOperationResult.None, null, response);
            }

            return result;
        }
    }
}
