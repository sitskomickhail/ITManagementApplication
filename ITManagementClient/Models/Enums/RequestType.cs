using System.ComponentModel;

namespace ITManagementClient.Models.Enums
{
    public enum RequestType
    {
        [Description("Отпуск")]
        Vacation = 1,
        [Description("Увольнение")]
        Dismission = 2,
        [Description("Неисправность")]
        Malfunction = 3,
        [Description("Предоставление доступа")]
        Access = 4
    }
}