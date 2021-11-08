using System.Windows.Input;
using ITManagementClient.Infrastructure;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class RegisterControlViewModel : BaseViewModel, IPageViewModel
    {
        public ICommand ReturnToLoginPage { get; set; }

        public RegisterControlViewModel()
        {
            ReturnToLoginPage = new RelayCommand(x => { Mediator.Notify(nameof(LoginControlViewModel), nameof(LoginControlViewModel)); });
        }
    }
}