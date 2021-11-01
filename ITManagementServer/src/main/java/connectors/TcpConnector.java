package connectors;

import com.google.gson.Gson;
import connectors.interfaces.ITcpConnector;
import models.transferModels.TransferRequestModel;
import models.transferModels.TransferResponseModel;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;

public class TcpConnector implements ITcpConnector {

    private int _port;

    ServerSocket serverSocket;
    Socket clientAccepted;

    ObjectInputStream inputStream;
    ObjectOutputStream outputStream;

    public TcpConnector(int port) {
        _port = port;
    }

    @Override
    public void StartConnection() throws IOException {
        serverSocket = new ServerSocket(_port);
        clientAccepted = serverSocket.accept();

        inputStream = new ObjectInputStream(clientAccepted.getInputStream());
        outputStream = new ObjectOutputStream(clientAccepted.getOutputStream());

    }

    @Override
    public TransferRequestModel GetClientMessage() throws IOException, ClassNotFoundException {
        inputStream = new ObjectInputStream(clientAccepted.getInputStream());
        String jsonObject = (String)inputStream.readObject();

        return new Gson().fromJson(jsonObject, TransferRequestModel.class);
    }

    @Override
    public void SendMessageToClient(TransferResponseModel transferObject) throws IOException {
        outputStream = new ObjectOutputStream(clientAccepted.getOutputStream());

        var jsonObject = new Gson().toJson(transferObject);
        outputStream.writeObject(jsonObject);
    }

    @Override
    public void CloseConnection() throws IOException {
        inputStream.close();
        outputStream.close();
        clientAccepted.close();
        serverSocket.close();
    }
}