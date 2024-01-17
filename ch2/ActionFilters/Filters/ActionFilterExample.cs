

using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilters.Filters;

public class ActionFilterExample : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // code before action executes


    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // code after action executes


    }
}
