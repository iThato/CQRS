using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.UserManagement.Events;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Helpers;
using Dvt.Infrastructure.Interfaces;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TokenType = Dvt.Features.Messages.Enums.TokenType;
using UserStatus = Dvt.Features.Messages.Enums.UserStatus;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class UserAddCommandRequestHandler : IRequestHandler<UserAddCommandRequest, OperationResult<UserAddCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IClock _clock;
        public UserAddCommandRequestHandler(DvtDatabaseContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<OperationResult<UserAddCommandResponse>> Handle(UserAddCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UserAddCommandResponse(request.MessageId);
            OperationResult<UserAddCommandResponse> result;

            var userAccount = await _dbContext.UserAccount.SingleOrDefaultAsync(u => u.Email == request.TransferObject.Email || u.ContactNumber == request.TransferObject.ContactNumber, cancellationToken: cancellationToken);
            if (userAccount.NotNull())
            {
                result = new OperationResult<UserAddCommandResponse>(EnumOperationResult.Duplicate,
                                                                     new List<ValidationError>
                                                                     {
                                                                         new ValidationError(nameof(request.TransferObject.Email),
                                                                                             "The User Email or Contact Number already exists")
                                                                     });
            }
            else
            {

                userAccount = new UserAccount
                {
                    Email = request.TransferObject.Email,
                    FirstName = request.TransferObject.FirstName,
                    LastName = request.TransferObject.LastName,
                    UserStatusId = (int)UserStatus.Disabled,
                    KnownAs = request.TransferObject.KnownAs,
                    Username = request.TransferObject.Email,
                    ContactNumber = request.TransferObject.ContactNumber,
                    SystemProfileId = request.TransferObject.SystemProfileId
                };


                await _dbContext.UserAccount.AddAsync(userAccount, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                var token = new Token
                {
                    Value = Guid.NewGuid(),
                    CreatedDate = _clock.NowAsSouthAfrican,
                    ExpiryDate = _clock.NowAsSouthAfrican.AddDays(1),
                    TokenTypeId = (int)TokenType.SetPassword,
                    UserId = userAccount.UserAccountId

                };

                await _dbContext.Token.AddAsync(token, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.UserAccountId = userAccount.UserAccountId;
                result = new OperationResult<UserAddCommandResponse>(EnumOperationResult.Ok, null, response);

                result.AddDomainEvent(new UserAddEvent
                {
                    UserAccountId = response.UserAccountId,

                });

            }

            return result;
        }
    }
}
