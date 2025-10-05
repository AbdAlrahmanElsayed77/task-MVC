using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using task.Models;

namespace task.Filters
{
    public class DepartmentLocationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("department"))
            {
                var dept = context.ActionArguments["department"] as Department;
                if (dept != null && dept.Location != "EG" && dept.Location != "USA")
                {
                    context.Result = new BadRequestObjectResult("Location must be EG or USA");
                }
            }
        }
    }
}
