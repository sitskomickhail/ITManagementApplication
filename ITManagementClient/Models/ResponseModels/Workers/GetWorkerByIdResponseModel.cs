using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITManagementClient.Models.ResponseModels.Workers
{
    public class GetWorkerByIdResponseModel
    {
        public int WorkerId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public bool Active { get; set; }

        public string EnglishLevel { get; set; }

        public string Login { get; set; }

        public string Department { get; set; }

        public int DepartmentId { get; set; }
    }
}