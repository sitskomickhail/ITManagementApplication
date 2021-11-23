package handlers.implementation.requests;

import constants.HandlerCodes;
import handlers.base.BaseRequestHandler;
import managers.RequestManager;
import managers.interfaces.IRequestManger;
import models.requestModels.requests.GetVacationAvailableDaysRequestModel;
import models.responseModels.requests.GetVacationAvailableDaysResponseModel;

import java.lang.reflect.Type;

public class GetAvailableUserVacationDaysRequestHandler extends BaseRequestHandler<GetVacationAvailableDaysRequestModel, GetVacationAvailableDaysResponseModel> {
    private IRequestManger _requestManger;

    public GetAvailableUserVacationDaysRequestHandler() {
        _requestManger = new RequestManager();
    }

    @Override
    public int GetHandlerCode() {
        return HandlerCodes.GET_AVAILABLE_VACATION_DAYS;
    }

    @Override
    public Type getIncomingModelType() {
        return GetVacationAvailableDaysRequestModel.class;
    }

    @Override
    protected GetVacationAvailableDaysResponseModel Execute(GetVacationAvailableDaysRequestModel model) throws Exception {
        var response = new GetVacationAvailableDaysResponseModel();

        var availableVacationDays = _requestManger.CalculateWorkerVacationDays(model.getWorkerId());
        response.setDaysAvailable(availableVacationDays);

        return response;
    }
}
