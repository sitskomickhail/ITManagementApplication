package models.responseModels.requests;

import java.sql.Date;

public class GridRequestHistoryModel {
    private int RequestId;
    private int RequestType;
    private Date CreateTime;
    private String RequestDescription;
    private int RequestStatus;

    public int getRequestId() {
        return RequestId;
    }

    public void setRequestId(int requestId) {
        RequestId = requestId;
    }

    public int getRequestType() {
        return RequestType;
    }

    public void setRequestType(int requestType) {
        RequestType = requestType;
    }

    public Date getCreateTime() {
        return CreateTime;
    }

    public void setCreateTime(Date createTime) {
        CreateTime = createTime;
    }

    public String getRequestDescription() {
        return RequestDescription;
    }

    public void setRequestDescription(String requestDescription) {
        RequestDescription = requestDescription;
    }

    public int getRequestStatus() {
        return RequestStatus;
    }

    public void setRequestStatus(int requestStatus) {
        RequestStatus = requestStatus;
    }
}