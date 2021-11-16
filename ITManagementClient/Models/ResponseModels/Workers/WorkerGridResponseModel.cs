using System;

namespace ITManagementClient.Models.ResponseModels.Workers
{
    public class WorkerGridResponseModel
    {
        public int WorkerId { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }
    }
}