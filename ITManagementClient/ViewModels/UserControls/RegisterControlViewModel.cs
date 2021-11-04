using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class RegisterControlViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand GoTo1 { get; set; }

        public RegisterControlViewModel()
        {
            GoTo1 = new RelayCommand(x =>
            {
                Mediator.Notify("OnGo1Screen");
            });
        }
    }
}