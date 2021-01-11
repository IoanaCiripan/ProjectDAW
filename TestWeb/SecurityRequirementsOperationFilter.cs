using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestWeb
{
  public class SecurityRequirementsOperationFilter : IOperationFilter
  {
    private readonly bool _includeUnauthorizedAndForbiddenResponses;

    public SecurityRequirementsOperationFilter(bool includeUnauthorizedAndForbiddenResponses = true)
    {
      _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
    }

    private IEnumerable<T> GetControllerAndActionAttributes<T>(OperationFilterContext context) where T : Attribute
    {
      IEnumerable<T> controllerAttributes = context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes<T>();
      IEnumerable<T> actionAttributes = context.MethodInfo.GetCustomAttributes<T>();
      List<T> result = new List<T>(controllerAttributes);
      result.AddRange(actionAttributes);
      return result;
    }

    public void Apply(Operation operation, OperationFilterContext context)
    {
      if (GetControllerAndActionAttributes<AllowAnonymousAttribute>(context).Any())
      {
        return;
      }

      IEnumerable<AuthorizeAttribute> actionAttributes = GetControllerAndActionAttributes<AuthorizeAttribute>(context);

      if (!actionAttributes.Any())
      {
        return;
      }

      if (_includeUnauthorizedAndForbiddenResponses)
      {
        operation.Responses.Add("401", new Response { Description = "Unauthorized" });
        operation.Responses.Add("403", new Response { Description = "Forbidden" });
      }

      IEnumerable<string> policies = actionAttributes
          .Where(a => !string.IsNullOrEmpty(a.Policy))
          .Select(a => a.Policy);

      operation.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>>
                {
                    { "Authorization Token", policies }
                }
            };
    }
  }
}