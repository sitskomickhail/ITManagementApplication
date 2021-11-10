using System;
using System.Windows.Controls;
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
    public class RegisterControlViewModel : BaseViewModel, IPageViewModel
    {
        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _repeatPassword;

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
            }
        }

        private string _adminPassword;

        public string AdministratorPassword
        {
            get => _adminPassword;
            set
            {
                _adminPassword = value;
                OnPropertyChanged(nameof(AdministratorPassword));
            }
        }

        public BaseActionHandler<RegisterRequestModel, RegisterResponseModel> RegisterActionHandler { get; }

        public ICommand ReturnToLoginPage { get; set; }

        public ICommand RegisterUserCommand { get; set; }

        public RegisterControlViewModel()
        {
            Login = "";
            Password = "";
            RepeatPassword = "";
            AdministratorPassword = "";

            RegisterActionHandler = new RegisterWorkerActionHandler();

            ReturnToLoginPage = new RelayCommand(ReturnBackCommandExecute);
            RegisterUserCommand = new RelayCommand(RegisterUserCommandExecute);
        }

        private void RegisterUserCommandExecute(object obj)
        {
            if (Password.Length < 8 || RepeatPassword.Length < 8)
            {
                Mediator.Notify("SnackbarMessageShow", "Длина пароля должна быть не меньше 8");
                return;
            }

            if (!Password.Equals(RepeatPassword, StringComparison.Ordinal))
            {
                Mediator.Notify("SnackbarMessageShow", "Введенные пароли не совпадают!");
                return;
            }

            if (String.IsNullOrEmpty(AdministratorPassword) || String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(RepeatPassword))
            {
                Mediator.Notify("SnackbarMessageShow", "Заполните все обязательные поля");
                return;
            }

            try
            {
                RegisterActionHandler.ExecuteHandler(new RegisterRequestModel()
                {
                    Password = Password,
                    Login = Login,
                    AdministerPassword = AdministratorPassword
                });

                ReturnToLoginPage.Execute(null);
            }
            catch { }

        }

        private void ReturnBackCommandExecute(object obj)
        {
            Mediator.Notify(nameof(LoginControlViewModel), nameof(LoginControlViewModel));
        }
    }
}