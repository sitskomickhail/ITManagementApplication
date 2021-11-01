package connectors.interfaces;

import models.transferModels.TransferRequestModel;
import models.transferModels.TransferResponseModel;

import java.io.IOException;

public interface ITcpConnector {
    void StartConnection() throws IOException;

    TransferRequestModel GetClientMessage() throws IOException, ClassNotFoundException;

    void SendMessageToClient(TransferResponseModel transferObject) throws IOException;

    void CloseConnection() throws IOException;
}