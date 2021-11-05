using System.Net.Sockets;
using System.Text;
using ITManagementClient.Models.TransferModels;
using ITManagementClient.Services.Interfaces;
using Newtonsoft.Json;

namespace ITManagementClient.Services
{
    public class TcpService : ITcpService
    {
        private Socket ClientSocket { get; set; }

        public TcpService()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void CreateConnection(string ip, int port)
        {
            ClientSocket.Connect(ip, port);

        }

        public void WriteStream(TransferRequestModel requestModel)
        {
            string json = JsonConvert.SerializeObject(requestModel);

            byte[] data = Encoding.UTF8.GetBytes(json);
            ClientSocket.Send(data);
            ClientSocket.Disconnect(false);
        }

        public TransferResponseModel ReadStream()
        {
            byte[] data = new byte[100000];

            int bytesRead = ClientSocket.Receive(data);
            string message = Encoding.UTF8.GetString(data, 0, bytesRead);

            var response = JsonConvert.DeserializeObject<TransferResponseModel>(message);
            return response;
        }

        public void DisposeConnection()
        {
            ClientSocket.Close();
        }
    }
}