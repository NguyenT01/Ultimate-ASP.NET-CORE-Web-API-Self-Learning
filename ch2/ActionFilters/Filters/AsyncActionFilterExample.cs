

using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilters.Filters;

public class AsyncActionFilterExample : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // execute code before action executes


        var result = await next();

        // execute code after action executes


    }
}
