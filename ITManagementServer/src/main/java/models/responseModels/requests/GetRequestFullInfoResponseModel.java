package models.responseModels.requests;

public class GetRequestFullInfoResponseModel {
    private String RequestDescription;
    private int RequestType;
    private int RequestStatus;
    private String ResolveNotes;

    public String getRequestDescription() {
        return RequestDescription;
    }

    public void setRequestDescription(String requestDescription) {
        RequestDescription = requestDescription;
    }

    public int getRequestType() {
        return RequestType;
    }

    public void setRequestType(int requestType) {
        RequestType = requestType;
    }

    public int getRequestStatus() {
        return RequestStatus;
    }

    public void setRequestStatus(int requestStatus) {
        RequestStatus = requestStatus;
    }

    public String getResolveNotes() {
        return ResolveNotes;
    }

    public void setResolveNotes(String resolveNotes) {
        ResolveNotes = resolveNotes;
    }
}