using System.Security.Claims;

namespace BelajarWebApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ClaimsPrincipal _user;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext.User;
        }

        public string GetFullname()
        {
            var fullname = _user.FindFirst(ClaimTypes.GivenName).Value;

            return fullname;
        }

        public long GetUserId()
        {
            var id = _user.FindFirst("id").Value;
            
            return int.Parse(id);
        }

        public string GetUsername()
        {
            var username = _user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return username;
        }
    }
}
