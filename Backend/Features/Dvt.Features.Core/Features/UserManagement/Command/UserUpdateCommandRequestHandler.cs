using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Structures;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class UserUpdateCommandRequestHandler : IRequestHandler<UserUpdateCommandRequest, OperationResult<UserUpdateCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public UserUpdateCommandRequestHandler(DvtDatabaseContext dbContex)
        {
            _dbContext = dbContex;
        }
        public async Task<OperationResult<UserUpdateCommandResponse>> Handle(UserUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UserUpdateCommandResponse(request.MessageId);

            OperationResult<UserUpdateCommandResponse> result;

            var userAccount = await _dbContext.UserAccount.FindAsync(request.TransferObject.UserAccountId);
            if (userAccount.NotNull())
            {
                userAccount.UserAccountId = request.TransferObject.UserAccountId;
                userAccount.FirstName = request.TransferObject.FirstName;
                userAccount.LastName = request.TransferObject.LastName;
                userAccount.ContactNumber = request.TransferObject.ContactNumber;
                userAccount.Email = request.TransferObject.Email;
                userAccount.KnownAs = request.TransferObject.KnownAs;
                userAccount.SystemProfileId = request.TransferObject.SystemProfileId;

                _dbContext.UserAccount.Update(userAccount);
                await _dbContext.SaveChangesAsync();


                response.UserAccountId = userAccount.UserAccountId;

                result = new OperationResult<UserUpdateCommandResponse>(EnumOperationResult.Updated, null, response);

            }
            else
            {
                result = new OperationResult<UserUpdateCommandResponse>(EnumOperationResult.None, null, response);
            }

            return result;

        }
    }
}
