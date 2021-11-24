using ITManagementClient.Models.Common;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.Managers
{
    public static class UserManager
    {
        private static ConnectedUserModel ConnectedUserModel { get; set; }

        public static ConnectedUserModel GetCurrentConnectedUser()
        {
            return ConnectedUserModel;
        }

        public static void SetCurrentConnectedUser(string login, int id, UserRoles role)
        {
            if (ConnectedUserModel == null)
            {
                ConnectedUserModel = new ConnectedUserModel();
            }

            ConnectedUserModel.Id = id;
            ConnectedUserModel.Login = login;
            ConnectedUserModel.Role = role;
        }

        public static void DisconnectCurrentUser()
        {
            ConnectedUserModel = null;
        }
    }
}