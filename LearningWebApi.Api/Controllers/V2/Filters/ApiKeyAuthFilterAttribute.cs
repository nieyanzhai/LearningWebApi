using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Api.Controllers.V2.Filters;

public class ApiKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var clientApiKey = context.HttpContext.Request.Headers["ApiKey"].FirstOrDefault();
        if (clientApiKey is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Get server api key
        var serverApiKey = context.HttpContext.RequestServices.GetService<IConfiguration>()["ApiKey:BlazorWasm"];
        if (clientApiKey != serverApiKey) context.Result = new UnauthorizedResult();
    }
}