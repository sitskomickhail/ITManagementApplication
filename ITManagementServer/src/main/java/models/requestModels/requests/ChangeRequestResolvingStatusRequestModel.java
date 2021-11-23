package models.requestModels.requests;

public class ChangeRequestResolvingStatusRequestModel {
    private int RequestId;
    private int RequestStatus;

    public int getRequestId() {
        return RequestId;
    }

    public void setRequestId(int requestId) {
        RequestId = requestId;
    }

    public int getRequestStatus() {
        return RequestStatus;
    }

    public void setRequestStatus(int requestStatus) {
        RequestStatus = requestStatus;
    }
}