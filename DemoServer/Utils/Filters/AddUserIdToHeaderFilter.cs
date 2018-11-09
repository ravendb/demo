using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoServer.Utils.Filters
{
    public class AddUserIdToHeaderAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var userIdFromRequest = GetHeaderFromRequest(context, HeaderKeys.DemoUserId);

            Guid.TryParse(userIdFromRequest, out var userId);
            if (userId == Guid.Empty)
                userId = Guid.NewGuid();

            SetValueInHeaders(context, HeaderKeys.DemoUserId, userId.ToString());
        }

        private string GetHeaderFromRequest(ResultExecutingContext context, string key) =>
            context.HttpContext.Request.Headers[key];

        private void SetValueInHeaders(ResultExecutingContext context, string key, string value) =>
            context.HttpContext.Response.Headers[key] = value;
    }
}
