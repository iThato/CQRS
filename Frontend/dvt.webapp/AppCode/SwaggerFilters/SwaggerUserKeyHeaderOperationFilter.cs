using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using wtw.webapp.AppCode.Attributes;

namespace wtw.webapp.AppCode.SwaggerFilters
{
    public class AuthorisationKeyHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //var isOptional = context?.ControllerActionDescriptor?.GetControllerAndActionAttributes(true)?.OfType<UserKeyOptionalAttribute>().Any() ?? false;
            //var isRequired = context?.ControllerActionDescriptor?.GetControllerAndActionAttributes(true)?.OfType<UserKeyRequiredAttribute>().Any() ?? false;

            //if (isOptional || isRequired)
            //{
            //    if (operation.Parameters == null)
            //        operation.Parameters = new List<IParameter>();
            //    operation.Parameters.Add(new NonBodyParameter
            //    {
            //        Name = "Authorisation",
            //        In = "header",
            //        Description = "access token",
            //        Required = isRequired,
            //        Type = "string",
            //        Default = isRequired ? "Enter your authorisation token." : ""
            //    });
            //}
        }
    }
}
