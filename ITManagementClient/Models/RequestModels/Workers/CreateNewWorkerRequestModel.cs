using System;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Workers 
{
    public class CreateNewWorkerRequestModel
    {
        public string Name { get; set; }

        public UserRoles Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}