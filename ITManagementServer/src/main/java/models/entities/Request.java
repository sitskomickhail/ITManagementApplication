package models.entities;

import java.sql.Date;

public class Request {
    private int id;
    private Date createTime;
    private int type;
    private String requestDescription;
    private int resolveStatus;
    private String resolveNote;
    private int workerId;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Date getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Date createTime) {
        this.createTime = createTime;
    }

    public int getType() {
        return type;
    }

    public void setType(int type) {
        this.type = type;
    }

    public String getRequestDescription() {
        return requestDescription;
    }

    public void setRequestDescription(String description) {
        this.requestDescription = description;
    }

    public int getResolveStatus() { return resolveStatus; }

    public void setResolveStatus(int resolveStatus) { this.resolveStatus = resolveStatus; }

    public String getResolveNote() {
        return resolveNote;
    }

    public void setResolveNote(String resolveNote) {
        this.resolveNote = resolveNote;
    }

    public int getWorkerId() {
        return workerId;
    }

    public void setWorkerId(int workerId) {
        this.workerId = workerId;
    }
}