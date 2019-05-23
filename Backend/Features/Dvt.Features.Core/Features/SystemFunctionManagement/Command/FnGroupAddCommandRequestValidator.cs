using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.SystemFunctionManagement.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Command
{
    public sealed class FnGroupAddCommandRequestValidator: AbstractValidator<FnGroupAddCommandRequest>
    {
        public FnGroupAddCommandRequestValidator()
        {
            RuleFor(x => x.TransferObject).NotNull();
            When(x => x.TransferObject.NotNull(), () =>
            {
                RuleFor(x => x.TransferObject.Name).NotEmpty();
            });
        }
    }
}
