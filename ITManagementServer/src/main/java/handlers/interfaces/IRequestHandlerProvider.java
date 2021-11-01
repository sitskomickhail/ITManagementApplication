package handlers.interfaces;

import models.transferModels.TransferRequestModel;
import models.transferModels.TransferResponseModel;

public interface IRequestHandlerProvider {
    TransferResponseModel Execute(TransferRequestModel requestModel);
}