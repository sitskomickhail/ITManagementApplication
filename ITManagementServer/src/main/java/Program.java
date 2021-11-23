import com.google.gson.Gson;
import connectors.TcpConnector;
import connectors.interfaces.ITcpConnector;
import constants.ExecutionResults;
import constants.HandlerCodes;
import handlers.HandlerThread;
import helpers.ConfigHelper;
import helpers.RandomHelper;
import models.responseModels.connectors.StartConnectionResponseModel;
import models.transferModels.TransferResponseModel;
import models.transferModels.responses.SuccessTransferResponseModel;

import java.io.IOException;

public class Program {
    public static void main(String[] args) throws IOException {
        ConfigHelper configHelper = new ConfigHelper();
        String portConfig = null;

        try {
            portConfig = configHelper.GetPropertyValue("tcpPort");
        } catch (IOException e) {
            e.printStackTrace();
        }

        if (portConfig == null || portConfig.isEmpty()) {
            System.out.println("Порт не получен...");
            return;
        }

        int tcpPort = Integer.parseInt(portConfig);
        ITcpConnector initialTcpConnector = new TcpConnector(tcpPort);

        System.out.println("Server started on 127.0.0.1:" + portConfig + "!");
        try {
            while (true) {
                System.out.println("Listening for client");
                initialTcpConnector.StartConnection();
                System.out.println("New client accepted");
                var clientMessage = initialTcpConnector.GetClientMessage();

                if (clientMessage.ActionCode == HandlerCodes.START_CONNECTION) {
                    System.out.println("Creating new thread for client");
                    int connectionPort = RandomHelper.Randomize();

                    SuccessTransferResponseModel<StartConnectionResponseModel> connectionModel = new SuccessTransferResponseModel<>();
                    connectionModel.responseModel = new StartConnectionResponseModel();
                    connectionModel.responseModel.setPort(connectionPort);

                    TransferResponseModel response = new TransferResponseModel();
                    response.executionCode = ExecutionResults.SUCCESS_CODE;
                    response.executionResult = new Gson().toJson(connectionModel);

                    initialTcpConnector.SendMessageToClient(response);

                    System.out.println("Client will run on port " + connectionPort);
                    Thread handlerThread = new Thread(new HandlerThread(connectionPort));
                    handlerThread.start();
                }

                Thread.sleep(3000);
                initialTcpConnector.CloseConnection();
                System.out.println("Connection closed");
            }
        } catch (IOException | ClassNotFoundException | InterruptedException e) {
            e.printStackTrace();
        } finally {
            initialTcpConnector.CloseConnection();
        }
    }
}