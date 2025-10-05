using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace task.Filters
{
    public class AddFooterFilterAttribute : Attribute,IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var response = context.HttpContext.Response;
            if (response.ContentType != null && response.ContentType.Contains("text/html"))
            {
                var footer = "<footer><p style='color:gray;text-align:center'>Generated at " + DateTime.Now + "</p></footer>";
                var bytes = Encoding.UTF8.GetBytes(footer);
                response.Body.Write(bytes, 0, bytes.Length);
                Console.WriteLine(DateTime.Now);
            }
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}
