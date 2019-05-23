using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
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
    public sealed class SysProfileFunctionLinkCommandRequestHandler : IRequestHandler<SysProfileFunctionLinkCommandRequest, OperationResult<SysProfileFunctionLinkCommandResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IClock _clock;

        public SysProfileFunctionLinkCommandRequestHandler(DvtDatabaseContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<OperationResult<SysProfileFunctionLinkCommandResponse>> Handle(SysProfileFunctionLinkCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SysProfileFunctionLinkCommandResponse(request.MessageId);
            OperationResult<SysProfileFunctionLinkCommandResponse> result;

            //Validate Unique Name
            var functionLink = await _dbContext.SystemProfileFunction.SingleOrDefaultAsync(u => u.SystemProfileId == request.TransferObject.ProfileId && u.SystemFunctionId == request.TransferObject.FunctionId);
            if (functionLink.NotNull())
            {
                result = new OperationResult<SysProfileFunctionLinkCommandResponse>(EnumOperationResult.Duplicate,
                                                                     new List<ValidationError>
                                                                     {
                                                                         new ValidationError(nameof(request.TransferObject.FunctionId),
                                                                                             "System Function is already Linked to Profile")
                                                                     });
            }
            else
            {

                functionLink = new SystemProfileFunction
                {
                    SystemFunctionId = request.TransferObject.FunctionId,
                    SystemProfileId = request.TransferObject.ProfileId
                };


                await _dbContext.SystemProfileFunction.AddAsync(functionLink, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                result = new OperationResult<SysProfileFunctionLinkCommandResponse>(EnumOperationResult.Ok, null, response);

                //result.AddDomainEvent(new SystemFunctionAddEvent
                //{
                //    Id = response.FunctionId,

                //});
            }

            return result;
        }
    }
}
