using System;
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

            int toSendLen = System.Text.Encoding.ASCII.GetByteCount(json);
            byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(json);
            byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
            ClientSocket.Send(toSendLenBytes);
            ClientSocket.Send(toSendBytes);
        }

        public TransferResponseModel ReadStream()
        {
            byte[] rcvLenBytes = new byte[4];
            ClientSocket.Receive(rcvLenBytes);
            int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            ClientSocket.Receive(rcvBytes);
            String message = System.Text.Encoding.ASCII.GetString(rcvBytes);

            var response = JsonConvert.DeserializeObject<TransferResponseModel>(message);
            return response;
        }

        public void DisposeConnection()
        {
            ClientSocket.Close();
        }
    }
}