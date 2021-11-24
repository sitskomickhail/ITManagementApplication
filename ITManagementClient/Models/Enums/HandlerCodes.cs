﻿// ReSharper disable InconsistentNaming
namespace ITManagementClient.Models.Enums
{
    public enum HandlerCodes
    {
        START_CONNECTION = 1,
        CLOSE_CONNECTION = 2,
        LOGIN_WORKER = 3,
        REGISTER_WORKER = 4,
        GET_WORKER_BY_ID = 5,
        GET_WORKERS_LIST = 6,
        UPDATE_WORKER = 7,
        CREATE_WORKER = 8,
        GET_PROJECTS_LIST = 9,
        GET_PROJECT_BY_ID = 10,
        CREATE_REQUEST = 11,
        GET_AVAILABLE_VACATION_DAYS = 12,
        GET_USER_REQUESTS_HISTORY = 13,
        GET_FULL_REQUEST_INFO = 14,
        CHANGE_REQUEST_STATUS = 15,
        FILTER_REQUESTS_LIST = 16,
        UPDATE_REQUEST = 17,
        GET_DEPARTMENTS_LIST = 18,
        UPDATE_DEPARTMENT = 19,
        CREATE_DEPARTMENT = 20,
        DELETE_DEPARTMENT = 21,
        GET_DEPARTMENT_FULL_INFO = 22,
        GET_DEVELOPER_PROJECTS = 23,
    }
}