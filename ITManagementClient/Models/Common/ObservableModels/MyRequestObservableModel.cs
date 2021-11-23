using System.Windows;
using System.Windows.Input;

namespace ITManagementClient.Models.Common.ObservableModels
{
    public class MyRequestObservableModel
    {
        public int Number { get; set; }

        public int RequestId { get; set; }

        public string RequestType { get; set; }

        public string CreationDate { get; set; }

        public string RequestDescription { get; set; }

        public string RequestStatus { get; set; }

        public ICommand ShowMoreInfoCommand { get; set; }

        public ICommand CancelRequestCommand { get; set; }

        public Visibility CancelButtonVisible { get; set; }
    }
}