namespace VehicleMaintenance.Business.Abstract
{
    public interface IUserSessionService
    {
        string GetRoleId();

        string GetRoleName();

        string GetRoles();

        string GetEmail();

        string GetUserName();

        int GetUserId();

        string GetIpAddress();

        string GetUserFullName();
    }
}