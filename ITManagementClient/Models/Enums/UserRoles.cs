using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITManagementClient.Models.Enums
{
    public enum UserRoles
    {
        [Description("Разработчик")]
        Developer = 1,
        [Description("HR-менеджер")]
        HrManager = 2,
        [Description("Ресурсный менеджер")]
        ResourceManager = 3,
        [Description("Системный администратор")]
        Administrator = 99999
    }
}
