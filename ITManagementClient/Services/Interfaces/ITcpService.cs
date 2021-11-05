using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Services.Interfaces
{
    public interface ITcpService
    {
        void CreateConnection(string ip, int port);

        void WriteStream(TransferRequestModel requestModel);

        TransferResponseModel ReadStream();

        void DisposeConnection();
    }
}