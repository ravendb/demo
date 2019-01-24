using System;

namespace DemoServer.Utils.UserId
{
    public class UserIdContainer
    {
        private readonly HeadersAccessor _headersAccessor;
        private readonly Lazy<Guid> _userIdLazy;

        public UserIdContainer(HeadersAccessor headersAccessor)
        {
            _headersAccessor = headersAccessor;
            _userIdLazy = new Lazy<Guid>(GetGuidFromRequest);
        }

        public Guid Get() => _userIdLazy.Value;

        private Guid GetGuidFromRequest()
        {
            var userIdFromRequest = _headersAccessor.GetUserIdFromRequest();
            return userIdFromRequest ?? Guid.NewGuid();
        }
    }
}
