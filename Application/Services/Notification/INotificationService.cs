using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.Notification.Notification.Response;

namespace Application.Services.Notification
{
    public interface INotificationService
    {
        Task<IBusinessLogicResult<bool>> NewNotification(
            RequestNewNotificationViewModel requestNewNotificationViewModel, int userId);
        
        Task<IBusinessLogicResult<bool>> EditNotification(
            RequestEditNotificationViewModel requestEditNotificationViewModel, int userId);
        
        Task<IBusinessLogicResult<ResponseGetNotificationListViewModel>> GetNotificationList(
            RequestGetNotificationViewModel requestGetNotificationViewModel, int userId);
        
        Task<IBusinessLogicResult<bool>> DeleteNotification(
            int notificationId, int userId);
    }
}