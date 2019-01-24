using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoServer.Utils.Filters
{
    public class AddUserIdToResponseHeaderAttribute : ResultFilterAttribute
    {
        private readonly UserIdContainer _userId;

        public AddUserIdToResponseHeaderAttribute(UserIdContainer userId)
        {
            _userId = userId;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var userIdToSet = _userId.Get();
            SetValueInResponseHeaders(context, HeaderKeys.DemoUserId, userIdToSet.ToString());
        }

        private void SetValueInResponseHeaders(ResultExecutingContext context, string key, string value) =>
            context.HttpContext.Response.Headers[key] = value;
    }
}
