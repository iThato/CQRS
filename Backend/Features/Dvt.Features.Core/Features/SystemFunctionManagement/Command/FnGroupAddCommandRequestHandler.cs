using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.SystemFunctionManagement.Messages;
using Dvt.Features.Core.Features.SystemFunctionManagement.Events;
using Dvt.Infrastructure.Interfaces;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Command
{
    public sealed class FnGroupAddCommandRequestHandler : IRequestHandler<FnGroupAddCommandRequest, OperationResult<FnGroupAddCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IClock _clock;

        public FnGroupAddCommandRequestHandler(DvtDatabaseContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<OperationResult<FnGroupAddCommandResponse>> Handle(FnGroupAddCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new FnGroupAddCommandResponse(request.MessageId);
            OperationResult<FnGroupAddCommandResponse> result;

            //Validate Unique Name
            var group = await _dbContext.SystemFunctionGroup.SingleOrDefaultAsync(u => u.Name == request.TransferObject.Name);
            if (group.NotNull())
            {
                result = new OperationResult<FnGroupAddCommandResponse>(EnumOperationResult.Duplicate,
                                                                     new List<ValidationError>
                                                                     {
                                                                         new ValidationError(nameof(request.TransferObject.Name),
                                                                                             "Group Name already exists")
                                                                     });
            }
            else
            {

                group = new SystemFunctionGroup
                {
                    DisplayName = request.TransferObject.DisplayName,
                    Name = request.TransferObject.Name,
                };
                

                await _dbContext.SystemFunctionGroup.AddAsync(group, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                result = new OperationResult<FnGroupAddCommandResponse>(EnumOperationResult.Ok, null, response);

                result.AddDomainEvent(new SystemFunctionGroupAddEvent
                {
                    GroupId = response.GroupId,

                });
            }

            return result;
        }
    }
}
