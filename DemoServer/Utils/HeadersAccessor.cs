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

            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"There was no {HeaderKeys.DemoUserId} entry in header.");

            var result = Guid.Parse(value);
            return result;
        }
    }
}
