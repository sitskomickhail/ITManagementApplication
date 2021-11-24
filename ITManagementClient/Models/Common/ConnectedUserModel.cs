using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.Common
{
    public class ConnectedUserModel
    {
        public string Login { get; set; }

        public int Id { get; set; }

        public UserRoles Role { get; set; }
    }
}