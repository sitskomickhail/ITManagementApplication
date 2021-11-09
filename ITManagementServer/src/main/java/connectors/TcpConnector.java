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

    ObjectInputStream inputStream = null;
    ObjectOutputStream outputStream = null;
    InputStream is;
    OutputStream os;

    public TcpConnector(int port) {
        _port = port;
    }

    @Override
    public void StartConnection() throws IOException {
        serverSocket = new ServerSocket(_port);
        clientAccepted = serverSocket.accept();

        is = clientAccepted.getInputStream();
        os = clientAccepted.getOutputStream();
    }

    @Override
    public TransferRequestModel GetClientMessage() throws IOException {
        byte[] lenBytes = new byte[4];
        is.read(lenBytes, 0, 4);
        int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
        byte[] receivedBytes = new byte[len];
        is.read(receivedBytes, 0, len);
        String jsonObject = new String(receivedBytes, 0, len);

        return new Gson().fromJson(jsonObject, TransferRequestModel.class);
    }

    @Override
    public void SendMessageToClient(TransferResponseModel transferObject) throws IOException {
        var jsonObject = new Gson().toJson(transferObject);

        byte[] toSendBytes = jsonObject.getBytes();
        int toSendLen = toSendBytes.length;
        byte[] toSendLenBytes = new byte[4];
        toSendLenBytes[0] = (byte) (toSendLen & 0xff);
        toSendLenBytes[1] = (byte) ((toSendLen >> 8) & 0xff);
        toSendLenBytes[2] = (byte) ((toSendLen >> 16) & 0xff);
        toSendLenBytes[3] = (byte) ((toSendLen >> 24) & 0xff);
        os.write(toSendLenBytes);
        os.write(toSendBytes);
    }

    @Override
    public void CloseConnection() throws IOException {
        clientAccepted.close();
        serverSocket.close();
    }
}