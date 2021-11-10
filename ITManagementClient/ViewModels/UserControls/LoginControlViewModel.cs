using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Workers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Models.RequestModels.Workers;
using ITManagementClient.Models.ResponseModels.Workers;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.UserControls
{
    public class LoginControlViewModel : BaseViewModel, IPageViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand NavigateToRegisterPage { get; set; }
        public ICommand LoginCommand { get; set; }
        public BaseActionHandler<LoginRequestModel, LoginResponseModel> LoginActionHandler { get; set; }
        
        public LoginControlViewModel()
        {
            Login = "";
            Password = "";

            LoginActionHandler = new LoginWorkerActionHandler();

            NavigateToRegisterPage = new RelayCommand(NavigateToPageCommandExecute);
            LoginCommand = new RelayCommand(LoginCommandExecute);
        }

        private void LoginCommandExecute(object obj)
        {
            try
            {
                var actionResult = LoginActionHandler.ExecuteHandler(new LoginRequestModel
                {
                    Password = Password,
                    Login = Login
                });

                Mediator.Notify("EnableUserManagementElements");
            }
            catch { }
        }

        private void NavigateToPageCommandExecute(object obj)
        {
            Mediator.Notify(nameof(RegisterControlViewModel), nameof(RegisterControlViewModel));
        }
    }
}