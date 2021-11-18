using System.Threading.Tasks;
using System.Windows;
using ITManagementClient.Handlers.Base;
using ITManagementClient.Handlers.Connectors;
using ITManagementClient.Managers;
using ITManagementClient.Models.RequestModels.Connectors;
using ITManagementClient.Models.ResponseModels.Connectors;
using ITManagementClient.ViewModels;
using ITManagementClient.Views;

namespace ITManagementClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BaseActionHandler<StartConnectionRequestModel, StartConnectionResponseModel> actionHandler = new StartConnectionActionHandler();

            var connectionResponse = await actionHandler.ExecuteHandler(new StartConnectionRequestModel());
            TcpHandlerManager.GetTcpHandlerManager().SetTcpPort(connectionResponse.Port);

            MainWindow app = new MainWindow();
            MainWindowViewModel context = new MainWindowViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}