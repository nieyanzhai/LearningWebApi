using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Api.Controllers.V2.Filters;

public class TicketsDescriptionActionFilter:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var description = context.HttpContext.Request.Query["description"];
        if (string.IsNullOrWhiteSpace(description))
        {
            context.Result = new BadRequestObjectResult("Description is required");
            return;
        }
        base.OnActionExecuting(context);
    }
}