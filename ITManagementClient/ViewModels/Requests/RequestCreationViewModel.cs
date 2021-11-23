using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Requests;
using ITManagementClient.Helpers;
using ITManagementClient.Infrastructure;
using ITManagementClient.Managers;
using ITManagementClient.Models.Common.ObservableModels;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.Requests
{
    public class RequestCreationViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition>
        {
            PageDefinition.Administrator, PageDefinition.Developer,
            PageDefinition.HumanResource, PageDefinition.ResourceManager
        };

        private double _availableVacationDays;
        public double AvailableVacationDays
        {
            get => _availableVacationDays;
            set { _availableVacationDays = value; OnPropertyChanged(nameof(AvailableVacationDays)); }
        }

        private string _vacationStartLimitDate;
        public string VacationStartLimitDate
        {
            get => DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
            set { _vacationStartLimitDate = value; OnPropertyChanged(VacationStartLimitDate); }
        }

        private DateTime _startVacationDate;
        public DateTime StartVacationDate
        {
            get => _startVacationDate;
            set { _startVacationDate = value; OnPropertyChanged(nameof(StartVacationDate)); }
        }

        private string _vacationDaysReserved;
        public string VacationDaysReserved
        {
            get => _vacationDaysReserved;
            set
            {
                if (Int32.TryParse(value, out _))
                {
                    var numericDaysReserved = Int32.Parse(value);
                    if (numericDaysReserved <= Math.Floor(AvailableVacationDays) && numericDaysReserved >= 0)
                    {
                        _vacationDaysReserved = value;
                        OnPropertyChanged(nameof(VacationDaysReserved));
                    }
                }
            }
        }

        private string _malfunctionRequestTheme;
        public string MalfunctionRequestTheme
        {
            get => _malfunctionRequestTheme;
            set { _malfunctionRequestTheme = value; OnPropertyChanged(MalfunctionRequestTheme); }
        }

        private string _malfunctionRequestDescription;
        public string MalfunctionRequestDescription
        {
            get => _malfunctionRequestDescription;
            set { _malfunctionRequestDescription = value; OnPropertyChanged(nameof(MalfunctionRequestDescription)); }
        }

        private string _accessRequestTheme;
        public string AccessRequestTheme
        {
            get => _accessRequestTheme;
            set { _accessRequestTheme = value; OnPropertyChanged(nameof(AccessRequestTheme)); }
        }

        private string _accessLink;
        public string AccessLink
        {
            get => _accessLink;
            set { _accessLink = value; OnPropertyChanged(nameof(AccessLink)); }
        }

        private string _accessNotes;
        public string AccessNotes
        {
            get => _accessNotes;
            set { _accessNotes = value; OnPropertyChanged(nameof(AccessNotes)); }
        }

        private string _requestDescription;
        public string RequestDescription
        {
            get => _requestDescription;
            set { _requestDescription = value; OnPropertyChanged(nameof(RequestDescription)); }
        }

        private string _requestType;
        public string RequestType
        {
            get => _requestType;
            set { _requestType = value; OnPropertyChanged(nameof(RequestType)); }
        }

        private string _requestStatus;
        public string RequestStatus
        {
            get => _requestStatus;
            set { _requestStatus = value; OnPropertyChanged(nameof(RequestStatus)); }
        }

        private string _resolveNotes;
        public string ResolveNotes
        {
            get => _resolveNotes;
            set { _resolveNotes = value; OnPropertyChanged(nameof(ResolveNotes)); }
        }

        private int _tabControlSelectedIndex;
        public int TabControlSelectedIndex
        {
            get => _tabControlSelectedIndex;
            set
            {
                _tabControlSelectedIndex = value;
                if (TabControlSelectedIndex == 2)
                {
                    GetListOfRequestsCommand.Execute(null);
                }
                OnPropertyChanged(nameof(TabControlSelectedIndex));
            }
        }


        public ObservableCollection<MyRequestObservableModel> MyRequestsList { get; set; }

        public ICommand CreateVacationRequestCommand { get; set; }
        public ICommand CreateDismissionRequestCommand { get; set; }
        public ICommand CreateMalfunctionRequestCommand { get; set; }
        public ICommand CreateAccessRequestCommand { get; set; }
        public ICommand GetListOfRequestsCommand { get; set; }

        public BaseActionHandler<CreateRequestRequestModel, CreateRequestResponseModel> CreateRequestActionHandler { get; set; }
        public BaseActionHandler<GetVacationAvailableDaysRequestModel, GetVacationAvailableDaysResponseModel> GetAvailableUserVacationDaysActionHandler { get; set; }
        public BaseActionHandler<GetUserRequestsHistoryRequestModel, GetUserRequestsHistoryResponseModel> GetUserRequestsHistoryActionHandler { get; set; }
        public BaseActionHandler<GetRequestFullInfoRequestModel, GetRequestFullInfoResponseModel> GetFullRequestInfoActionHandler { get; set; }
        public BaseActionHandler<ChangeRequestResolvingStatusRequestModel, ChangeRequestResolvingStatusResponseModel> ChangeRequestResolvingStatusActionHandler { get; set; }

        public RequestCreationViewModel()
        {
            MyRequestsList = new ObservableCollection<MyRequestObservableModel>();

            MyRequestsList.Add(new MyRequestObservableModel
            {
                ShowMoreInfoCommand = new RelayCommand(ShowMoreRequestInfoCommandExecute),
                CancelRequestCommand = new RelayCommand(CancelRequestCommandExecute)
            });

            StartVacationDate = DateTime.Now.AddDays(1);

            CreateVacationRequestCommand = new RelayCommand(CreateVacationRequestCommandExecute);
            CreateDismissionRequestCommand = new RelayCommand(CreateDismissionRequestCommandExecute);
            CreateMalfunctionRequestCommand = new RelayCommand(CreateMalfunctionRequestCommandExecute);
            CreateAccessRequestCommand = new RelayCommand(CreateAccessRequestCommandExecute);
            GetListOfRequestsCommand = new RelayCommand(GetListOfRequestsCommandExecute);

            CreateRequestActionHandler = new CreateRequestActionHandler();
            GetAvailableUserVacationDaysActionHandler = new GetAvailableUserVacationDaysActionHandler();
            GetUserRequestsHistoryActionHandler = new GetUserRequestsHistoryActionHandler();
            GetFullRequestInfoActionHandler = new GetFullRequestInfoActionHandler();
            ChangeRequestResolvingStatusActionHandler = new ChangeRequestResolvingStatusActionHandler();
        }

        public void LoadInstance()
        {
            try
            {
                var resultInstance = GetAvailableUserVacationDaysActionHandler.ExecuteHandler(new GetVacationAvailableDaysRequestModel
                {
                    WorkerId = UserManager.GetCurrentConnectedUser().Id
                });

                AvailableVacationDays = resultInstance.DaysAvailable;
            }
            catch { /**/ }
        }

        public void CreateVacationRequestCommandExecute(object obj)
        {
            try
            {
                if (Int32.Parse(VacationDaysReserved) == 0)
                {
                    Mediator.Notify("SnackbarMessageShow", "Количество дней отпуска должно быть > 0");
                    return;
                }

                if (DateTime.Parse(VacationStartLimitDate) < DateTime.Now)
                {
                    Mediator.Notify("SnackbarMessageShow", "Некорректная дата начала отпуска");
                    return;
                }

                CreateRequestActionHandler.ExecuteHandler(new CreateRequestRequestModel
                {
                    RequestType = Models.Enums.RequestType.Vacation,
                    WorkerId = UserManager.GetCurrentConnectedUser().Id,
                    RequestDescription = $"Отпуск начиная с {DateTime.Parse(VacationStartLimitDate):dd.MM.yyyy} на {VacationDaysReserved} дней"
                });

                Mediator.Notify("SnackbarMessageShow", "Заявка на отпуск успешно создана");
            }
            catch { /**/ }
        }

        public void CreateDismissionRequestCommandExecute(object obj)
        {
            try
            {
                CreateRequestActionHandler.ExecuteHandler(new CreateRequestRequestModel
                {
                    RequestType = Models.Enums.RequestType.Dismission,
                    WorkerId = UserManager.GetCurrentConnectedUser().Id,
                    RequestDescription = "Заявка на увольнение"
                });

                Mediator.Notify("SnackbarMessageShow", "Заявка на увольнение успешно создана");
            }
            catch { /**/ }
        }

        public void CreateMalfunctionRequestCommandExecute(object obj)
        {
            try
            {
                if (String.IsNullOrEmpty(MalfunctionRequestTheme) || String.IsNullOrEmpty(MalfunctionRequestDescription))
                {
                    Mediator.Notify("SnackbarMessageShow", "Заполните все обязательные поля для оформления заявки");
                    return;
                }

                CreateRequestActionHandler.ExecuteHandler(new CreateRequestRequestModel()
                {
                    RequestType = Models.Enums.RequestType.Malfunction,
                    WorkerId = UserManager.GetCurrentConnectedUser().Id,
                    RequestDescription = $"{MalfunctionRequestTheme}\n{MalfunctionRequestDescription}"
                });

                Mediator.Notify("SnackbarMessageShow", "Заявка сис-админу успешно создана");
            }
            catch { /**/ }
        }

        public void CreateAccessRequestCommandExecute(object obj)
        {
            try
            {
                if (String.IsNullOrEmpty(AccessRequestTheme) || String.IsNullOrEmpty(AccessLink))
                {
                    Mediator.Notify("SnackbarMessageShow", "Заполните все обязательные поля для оформления заявки");
                    return;
                }

                string requestDescription = $"{AccessRequestTheme}\n{AccessLink} ";
                requestDescription += !String.IsNullOrEmpty(AccessNotes) ? AccessNotes : String.Empty;

                CreateRequestActionHandler.ExecuteHandler(new CreateRequestRequestModel()
                {
                    RequestType = Models.Enums.RequestType.Dismission,
                    WorkerId = UserManager.GetCurrentConnectedUser().Id,
                    RequestDescription = requestDescription
                });

                Mediator.Notify("SnackbarMessageShow", "Заявка сис-админу успешно создана");
            }
            catch { /**/ }
        }

        public void GetListOfRequestsCommandExecute(object obj)
        {
            try
            {
                var actionResult = GetUserRequestsHistoryActionHandler.ExecuteHandler(new GetUserRequestsHistoryRequestModel
                {
                    WorkerId = UserManager.GetCurrentConnectedUser().Id
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    MyRequestsList.Clear();
                    int listCounter = 1;
                    foreach (var worker in actionResult.RequestsList)
                    {
                        MyRequestsList.Add(new MyRequestObservableModel
                        {
                            Number = listCounter++,
                            RequestType = worker.RequestType.GetDescription(),
                            RequestStatus = worker.RequestStatus.GetDescription(),
                            CreationDate = worker.CreateTime.ToString("dd.MM.yyyy"),
                            RequestDescription = worker.RequestDescription,
                            RequestId = worker.RequestId,
                            CancelButtonVisible = worker.RequestStatus == Models.Enums.RequestStatus.Opened ? Visibility.Visible : Visibility.Hidden,
                            CancelRequestCommand = new RelayCommand(CancelRequestCommandExecute),
                            ShowMoreInfoCommand = new RelayCommand(ShowMoreRequestInfoCommandExecute)
                        });
                    }
                });
            }
            catch { /**/ }
        }

        public void ShowMoreRequestInfoCommandExecute(object obj)
        {
            try
            {
                int requestId = (int)obj;
                var actionResult = GetFullRequestInfoActionHandler.ExecuteHandler(new GetRequestFullInfoRequestModel
                {
                    RequestId = requestId
                });

                RequestDescription = actionResult.RequestDescription;
                RequestStatus = actionResult.RequestStatus.GetDescription();
                RequestType = actionResult.RequestType.GetDescription();
                ResolveNotes = actionResult.ResolveNotes;
            }
            catch { /**/ }
        }

        public void CancelRequestCommandExecute(object obj)
        {
            try
            {
                int requestId = (int)obj;
                ChangeRequestResolvingStatusActionHandler.ExecuteHandler(new ChangeRequestResolvingStatusRequestModel
                {
                    RequestId = requestId,
                    RequestStatus = Models.Enums.RequestStatus.Canceled
                });

                GetListOfRequestsCommand.Execute(null);
            }
            catch { /**/ }
        }
    }
}