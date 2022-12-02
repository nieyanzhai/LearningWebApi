using LearningWebApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningWebApi.Api.Controllers.V2.Filters;

public class TicketsDescriptionActionFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments["ticket"] is Ticket ticket && !ticket.ValidateDescriptionExists())
        {
            context.ModelState.AddModelError("Description", "Description is required");
            context.Result = new BadRequestObjectResult(context.ModelState);
            return;
        }

        base.OnActionExecuting(context);
    }
}