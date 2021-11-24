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
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;

namespace ITManagementClient.ViewModels.Requests
{
    public class RequestsListViewModel : BaseViewModel, IControlViewModel
    {
        public IEnumerable<PageDefinition> PageDefinitions => new List<PageDefinition> { PageDefinition.Administrator, PageDefinition.HumanResource };

        public IEnumerable<string> StatusList { get; set; } = new List<string>
        {
            RequestStatus.Opened.GetDescription(),
            RequestStatus.InProgress.GetDescription(),
            RequestStatus.Solved.GetDescription(),
            RequestStatus.Declined.GetDescription()
        };

        private Dictionary<string, RequestStatus> _requestStatuses = new Dictionary<string, RequestStatus>
        {
            {RequestStatus.Opened.GetDescription(), RequestStatus.Opened},
            {RequestStatus.InProgress.GetDescription(), RequestStatus.InProgress},
            {RequestStatus.Solved.GetDescription(), RequestStatus.Solved},
            {RequestStatus.Declined.GetDescription(), RequestStatus.Declined},
        };

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

        private RequestStatus _statusChoice;
        public string SelectedRequestStatus
        {
            get => _statusChoice.GetDescription();
            set
            {
                _statusChoice = _requestStatuses[value];
                OnPropertyChanged(nameof(SelectedRequestStatus));
            }
        }

        private string _resolveNotes;
        public string ResolveNotes
        {
            get => _resolveNotes;
            set { _resolveNotes = value; OnPropertyChanged(nameof(ResolveNotes)); }
        }

        private Visibility _saveButtonVisibility;
        public Visibility SaveButtonVisibility
        {
            get => _saveButtonVisibility;
            set { _saveButtonVisibility = value; OnPropertyChanged(nameof(SaveButtonVisibility)); }
        }

        private int _editingRequestId;
        public int EditingRequestId
        {
            get => _editingRequestId;
            set { _editingRequestId = value; OnPropertyChanged(nameof(EditingRequestId)); }
        }

        public ObservableCollection<RequestsObservableModel> RequestsList { get; set; }

        public ICommand UpdateRequestCommand { get; set; }
        public ICommand GetListOfRequestsCommand { get; set; }

        public BaseActionHandler<FilterRequestsListRequestModel, FilterRequestsListResponseModel> FilterRequestsListActionHandler { get; set; }
        public BaseActionHandler<GetRequestFullInfoRequestModel, GetRequestFullInfoResponseModel> GetFullRequestInfoActionHandler { get; set; }
        public BaseActionHandler<UpdateRequestRequestModel, UpdateRequestResponseModel> UpdateRequestActionHandler { get; set; }

        public RequestsListViewModel()
        {
            RequestsList = new ObservableCollection<RequestsObservableModel>();

            GetListOfRequestsCommand = new RelayCommand(GetListOfRequestsCommandExecute);
            UpdateRequestCommand = new RelayCommand(UpdateRequestCommandExecute);

            FilterRequestsListActionHandler = new FilterRequestsListActionHandler();
            GetFullRequestInfoActionHandler = new GetFullRequestInfoActionHandler();
            UpdateRequestActionHandler = new UpdateRequestActionHandler();
        }

        public void LoadInstance()
        {
            GetListOfRequestsCommand.Execute(null);
        }

        public void GetListOfRequestsCommandExecute(object obj)
        {
            try
            {
                List<RequestType> filteringTypes = new List<RequestType>();

                if (UserManager.GetCurrentConnectedUser().Role == UserRoles.Administrator)
                {
                    filteringTypes.Add(Models.Enums.RequestType.Malfunction);
                    filteringTypes.Add(Models.Enums.RequestType.Access);
                }
                else
                {
                    filteringTypes.Add(Models.Enums.RequestType.Vacation);
                    filteringTypes.Add(Models.Enums.RequestType.Dismission);
                }

                var actionResult = FilterRequestsListActionHandler.ExecuteHandler(new FilterRequestsListRequestModel()
                {
                    FilteringTypes = filteringTypes
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    RequestsList.Clear();
                    int listCounter = 1;
                    foreach (var worker in actionResult.RequestsList)
                    {
                        RequestsList.Add(new RequestsObservableModel
                        {
                            Number = listCounter++,
                            RequestType = worker.RequestType.GetDescription(),
                            RequestStatus = worker.RequestStatus.GetDescription(),
                            CreationDate = worker.CreateTime.ToString("dd.MM.yyyy"),
                            RequestDescription = worker.RequestDescription,
                            RequestId = worker.RequestId,
                            ShowMoreInfoCommand = new RelayCommand(ShowMoreRequestInfoCommandExecute)
                        });
                    }
                });
            }
            catch { /**/ }
        }

        public void UpdateRequestCommandExecute(object obj)
        {
            try
            {
                UpdateRequestActionHandler.ExecuteHandler(new UpdateRequestRequestModel
                {
                    RequestId = EditingRequestId,
                    RequestStatus = _statusChoice,
                    ResolveNote = ResolveNotes
                });

                GetListOfRequestsCommand.Execute(null);
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
                _statusChoice = actionResult.RequestStatus;
                RequestType = actionResult.RequestType.GetDescription();
                ResolveNotes = actionResult.ResolveNotes;
                EditingRequestId = requestId;

                if (actionResult.RequestStatus != RequestStatus.InProgress &&
                    actionResult.RequestStatus != RequestStatus.Opened)
                {
                    SaveButtonVisibility = Visibility.Hidden;
                }
                else
                {
                    SaveButtonVisibility = Visibility.Visible;
                }

                OnPropertyChanged(nameof(SelectedRequestStatus));
            }
            catch { /**/ }
        }
    }
}