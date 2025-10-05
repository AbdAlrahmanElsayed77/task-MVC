using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace task.Filters
{
    public class CheckUserFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userHeader = context.HttpContext.Request.Headers["User"].ToString();
            if (userHeader != "Student")
            {
                context.Result = new ContentResult()
                {
                    Content = "Unauthorized - Only Students allowed",
                    StatusCode = 403
                };
            }
        }
    }
}
