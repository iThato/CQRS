using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.SystemFunctionManagement.Messages;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Query
{
    public sealed class GetAllSystemFunctionGroupsRequestHandler : IRequestHandler<GetAllSysFunctionGroupsQueryRequest, OperationResult<GetAllSysFunctionGroupsQueryResponse>>
    {
        private readonly DvtDatabaseContext _dbContext;
        public GetAllSystemFunctionGroupsRequestHandler(DvtDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<GetAllSysFunctionGroupsQueryResponse>> Handle(GetAllSysFunctionGroupsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllSysFunctionGroupsQueryResponse(request.MessageId);
            OperationResult<GetAllSysFunctionGroupsQueryResponse> result;

            var groups = await _dbContext.SystemFunctionGroup.Include("SystemFunction").ToListAsync();

            if (groups.NotNull() && groups.HasItems())
            {
                response.Result = groups.ConvertAll(g => new SystemGroupResponse
                {
                    DisplayName = g.DisplayName,
                    Name = g.Name,
                    SystemGroupId = g.SystemFunctionGroupId,
                    SystemFunctions = new List<string>(g.SystemFunction.Select(s => s?.Name))
                });
                result = new OperationResult<GetAllSysFunctionGroupsQueryResponse>(EnumOperationResult.Ok, null, response);
            }
            else
            {
                result = new OperationResult<GetAllSysFunctionGroupsQueryResponse>(EnumOperationResult.None);
            }

            return result;
        }
    }
}
