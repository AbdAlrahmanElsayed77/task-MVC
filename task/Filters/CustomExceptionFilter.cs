using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace task.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                Content = $"Error: {context.Exception.Message}",
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
