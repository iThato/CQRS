using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Helpers;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class AddPasswordCommandRequestHandler : IRequestHandler<AddPasswordCommandRequest, OperationResult<AddPasswordCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;

        public AddPasswordCommandRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;


        }

        public async Task<OperationResult<AddPasswordCommandResponse>> Handle(AddPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var salt = PasswordHelper.GeneratePasswordSalt();
            var response = new AddPasswordCommandResponse(request.MessageId);
            OperationResult<AddPasswordCommandResponse> result;

            var userAccount = await _dbContext.UserAccount.SingleOrDefaultAsync(u => u.Email == request.TransferObject.Email || u.Password == request.TransferObject.Password, cancellationToken: cancellationToken);
            if (userAccount.NotNull())
            {
                userAccount.Email = request.TransferObject.Email;
                userAccount.Password = PasswordHelper.GenerateSaltedHash(request.TransferObject.Password, salt);
                userAccount.Salt = salt;

                _dbContext.UserAccount.Update(userAccount);
                await _dbContext.SaveChangesAsync();

                response.UserId = userAccount.UserAccountId;
                result = new OperationResult<AddPasswordCommandResponse>(EnumOperationResult.Updated, null, response);
            }
            else
            {

                result = new OperationResult<AddPasswordCommandResponse>(EnumOperationResult.None, null, response);
            }

            return result;

        }

    }
}
