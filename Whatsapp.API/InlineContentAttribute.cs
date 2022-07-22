using Microsoft.AspNetCore.Mvc.Filters;

namespace Whatsapp.API
{
    public class InlineContentAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Content-Disposition", "inline");
            base.OnResultExecuting(context);
        }
    }
}