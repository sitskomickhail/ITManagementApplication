using System.Collections.Generic;

namespace ITManagementClient.Models.ResponseModels.Departments
{
    public class GetDepartmentsListResponseModel
    {
        public IEnumerable<DepartmentGridResponseModel> Departments { get; set; }
    }
}