using System.ComponentModel;

namespace ITManagementClient.Models.Enums
{
    public enum RequestStatus
    {
        [Description("Активно")]
        Opened = 1,
        [Description("Выполняется")]
        InProgress = 2,
        [Description("Завершено")]
        Solved = 3,
        [Description("Отменено")]
        Canceled = 4,
        [Description("Отклонено")]
        Declined = 5
    }
}