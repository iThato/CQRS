using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.SystemFunctionManagement.Events;
using Dvt.Features.Core.Features.SystemFunctionManagement.Messages;
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
    public sealed class SysFunctionGroupLinkCommandRequestHandler : IRequestHandler<SysFunctionGroupLinkCommandRequest, OperationResult<SysFunctionGroupLinkCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IClock _clock;

        public SysFunctionGroupLinkCommandRequestHandler(DvtDatabaseContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<OperationResult<SysFunctionGroupLinkCommandResponse>> Handle(SysFunctionGroupLinkCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SysFunctionGroupLinkCommandResponse(request.MessageId);
            OperationResult<SysFunctionGroupLinkCommandResponse> result;

            //Validate Unique Name
            var function = await _dbContext.SystemFunction.SingleOrDefaultAsync(u => u.SystemFunctionGroupId == request.TransferObject.GroupId);
            if (function.NotNull())
            {
                result = new OperationResult<SysFunctionGroupLinkCommandResponse>(EnumOperationResult.Duplicate,
                                                                     new List<ValidationError>
                                                                     {
                                                                         new ValidationError(nameof(request.TransferObject.GroupId),
                                                                                             "System Function is already Linked to Group")
                                                                     });
            }
            else
            {

                function = new SystemFunction
                {

                    //SystemFunctionGroupId = request.TransferObject.GroupId
                };


                await _dbContext.SystemFunction.AddAsync(function, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                result = new OperationResult<SysFunctionGroupLinkCommandResponse>(EnumOperationResult.Ok, null, response);

                result.AddDomainEvent(new SystemFunctionAddEvent
                {
                    Id = response.FunctionId,

                });
            }

            return result;
        }
    }
}
