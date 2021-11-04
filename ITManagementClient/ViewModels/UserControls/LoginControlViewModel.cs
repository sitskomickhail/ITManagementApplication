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
    public class LoginControlViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand GoTo2 { get; set; }

        public LoginControlViewModel()
        {
            GoTo2 = new RelayCommand(x =>
            {
                Mediator.Notify("OnGo2Screen");
            });
        }
    }
}