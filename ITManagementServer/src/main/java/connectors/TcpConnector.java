package connectors;

import com.google.gson.Gson;
import connectors.interfaces.ITcpConnector;
import models.transferModels.TransferRequestModel;
import models.transferModels.TransferResponseModel;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

public class TcpConnector implements ITcpConnector {

    private int _port;

    ServerSocket serverSocket;
    Socket clientAccepted;

    public TcpConnector(int port) {
        _port = port;
    }

    @Override
    public void StartConnection() throws IOException {
        serverSocket = new ServerSocket(_port);
        clientAccepted = serverSocket.accept();
    }

    @Override
    public TransferRequestModel GetClientMessage() throws IOException{
        var inputStream = new BufferedReader(new InputStreamReader(clientAccepted.getInputStream()));
        String jsonObject = inputStream.readLine();

        return new Gson().fromJson(jsonObject, TransferRequestModel.class);
    }

    @Override
    public void SendMessageToClient(TransferResponseModel transferObject) throws IOException {
        DataOutputStream outputStream = new DataOutputStream(clientAccepted.getOutputStream());

        var jsonObject = new Gson().toJson(transferObject);

        byte[] buffer = jsonObject.getBytes();
        outputStream.write(buffer, 0, buffer.length);
    }

    @Override
    public void CloseConnection() throws IOException {
        clientAccepted.close();
        serverSocket.close();
    }
}