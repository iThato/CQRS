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
    public sealed class SysFunctionAddCommandRequestHandler : IRequestHandler<SysFunctionAddCommandRequest, OperationResult<SysFunctionAddCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IClock _clock;

        public SysFunctionAddCommandRequestHandler(DvtDatabaseContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<OperationResult<SysFunctionAddCommandResponse>> Handle(SysFunctionAddCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SysFunctionAddCommandResponse(request.MessageId);
            OperationResult<SysFunctionAddCommandResponse> result;

            //Validate Unique Name
            var function = await _dbContext.SystemFunction.SingleOrDefaultAsync(u => u.Name == request.TransferObject.Name);
            if (function.NotNull())
            {
                result = new OperationResult<SysFunctionAddCommandResponse>(EnumOperationResult.Duplicate,
                                                                     new List<ValidationError>
                                                                     {
                                                                         new ValidationError(nameof(request.TransferObject.Name),
                                                                                             "System Function Name already exists")
                                                                     });
            }
            else
            {

                function = new SystemFunction
                {
                    DisplayName = request.TransferObject.DisplayName,
                    Name = request.TransferObject.Name,
                    SystemFunctionGroupId = request.TransferObject.GroupId
                };


                await _dbContext.SystemFunction.AddAsync(function, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                result = new OperationResult<SysFunctionAddCommandResponse>(EnumOperationResult.Ok, null, response);

                result.AddDomainEvent(new SystemFunctionAddEvent
                {
                    Id = response.FunctionId,

                });
            }

            return result;
        }
    }
}
