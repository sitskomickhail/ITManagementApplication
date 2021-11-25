using System.Collections.Generic;
using ITManagementClient.Models.ResponseModels.Workers;

namespace ITManagementClient.Models.ResponseModels.Departments
{
    public class GetFullDepartmentInfoResponseModel
    {
        public int DepartmentId { get; set; }

        public string Title { get; set; }

        public string WorkerDuties { get; set; }

        public IEnumerable<WorkerGridResponseModel> Workers { get; set; }
    }
}