namespace BelajarWebApi.Services
{
    public interface IIdentityService
    {
        long GetUserId();
        string GetUsername();
        string GetFullname();
    }
}
