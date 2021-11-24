using System.Collections.Generic;
using ITManagementClient.Models.Enums;

namespace ITManagementClient.ViewModels.Interfaces
{
    public interface IControlViewModel
    {
        IEnumerable<PageDefinition> PageDefinitions { get; }

        void LoadInstance();
    }
}