package models.requestModels.requests;

public class UpdateRequestRequestModel {
    private int RequestId;
    private String ResolveNote;
    private int RequestStatus;

    public int getRequestId() {
        return RequestId;
    }

    public void setRequestId(int requestId) {
        RequestId = requestId;
    }

    public String getResolveNote() {
        return ResolveNote;
    }

    public void setResolveNote(String resolveNote) {
        ResolveNote = resolveNote;
    }

    public int getRequestStatus() {
        return RequestStatus;
    }

    public void setRequestStatus(int requestStatus) {
        RequestStatus = requestStatus;
    }
}