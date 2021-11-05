using ITManagementClient.Services;
using ITManagementClient.Services.Interfaces;

namespace ITManagementClient.Managers
{
    public class TcpHandlerManager
    {
        private int _port;

        private ITcpService _tcpService;

        private static TcpHandlerManager _tcpHandlerManager;

        public TcpHandlerManager()
        {
            _port = 9119;
        }

        public ITcpService GetTcpServiceInstance()
        {
            if (_tcpService == null)
            {
                _tcpService = new TcpService();
                _tcpService.CreateConnection("127.0.0.1", _port);
            }

            return _tcpService;
        }

        public void SetTcpPort(int port)
        {
            _port = port;

            if (_tcpService != null)
            {
                _tcpService.DisposeConnection();
                _tcpService = null;
            }
        }

        public static TcpHandlerManager GetTcpHandlerManager()
        {
            if (_tcpHandlerManager == null)
            {
                _tcpHandlerManager = new TcpHandlerManager();
            }

            return _tcpHandlerManager;
        }
    }
}