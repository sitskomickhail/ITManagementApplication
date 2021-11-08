using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.ResponseModels.Workers 
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }

        public UserRoles UserRole { get; set; }
    }
}