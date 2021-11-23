using ITManagementClient.Handlers.Base;
using ITManagementClient.Models.Enums;
using ITManagementClient.Models.RequestModels.Requests;
using ITManagementClient.Models.ResponseModels.Requests;
using ITManagementClient.Models.TransferModels;

namespace ITManagementClient.Handlers.Requests
{
    public class GetAvailableUserVacationDaysActionHandler : BaseActionHandler<GetVacationAvailableDaysRequestModel, GetVacationAvailableDaysResponseModel>
    {
        public override HandlerCodes HandlerCode => HandlerCodes.GET_AVAILABLE_VACATION_DAYS;

        protected override TransferResponseModel HandleResult(GetVacationAvailableDaysRequestModel model)
        {
            var requestModel = CreateRequestModel(model);

            HandlerManager.GetTcpServiceInstance().WriteStream(requestModel);
            var response = HandlerManager.GetTcpServiceInstance().ReadStream();

            return response;
        }
    }
}