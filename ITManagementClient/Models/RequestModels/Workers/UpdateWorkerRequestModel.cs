using System;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.Models.RequestModels.Workers
{
    public class UpdateWorkerRequestModel
    {
        public int WorkerId { get; set; }

        public UserRoles Position { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Active { get; set; }

        public string EnglishLevel { get; set; }

        public string Login { get; set; }

        public int? DepartmentId { get; set; }
    }
}