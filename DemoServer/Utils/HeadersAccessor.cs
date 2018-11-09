using System;
using Microsoft.AspNetCore.Http;

namespace DemoServer.Utils
{
    public class HeadersAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpRequest Request => _httpContextAccessor.HttpContext.Request;
        private string GetRequestHeader(string key) => Request.Headers[key];

        public HeadersAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserIdFromRequest()
        {
            var value = GetRequestHeader(HeaderKeys.DemoUserId);
            Guid.TryParse(value, out var result);
            return result;
        }
    }
}
