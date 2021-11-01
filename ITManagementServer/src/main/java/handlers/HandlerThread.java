package handlers;

import connectors.TcpConnector;
import connectors.interfaces.ITcpConnector;
import constants.HandlerCodes;
import handlers.interfaces.IRequestHandlerProvider;

public class HandlerThread implements Runnable {

    ITcpConnector tcpConnector;
    IRequestHandlerProvider handlerProvider;

    public HandlerThread(int tcpPort) {
        tcpConnector = new TcpConnector(tcpPort);
        handlerProvider = new RequestHandlerProvider();
    }

    @Override
    public void run() {
        try {
            tcpConnector.StartConnection();

            while (true) {
                var clientMessage = tcpConnector.GetClientMessage();

                if(clientMessage.actionCode == HandlerCodes.CLOSE_CONNECTION) {
                    tcpConnector.CloseConnection();
                    break;
                }

                var handlerResult = handlerProvider.Execute(clientMessage);

                tcpConnector.SendMessageToClient(handlerResult);
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}