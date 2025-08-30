using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Attachment.Response;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.Notification.Notification.Response;

namespace Application.Services.Notification.Attachment
{
    public interface INotificationAttachmentService
    {
        Task<IBusinessLogicResult<bool>> NewNotificationAttachment(
            RequestNewNotificationAttachmentViewModel requestNewNotificationAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<bool>> EditNotificationAttachment(
            RequestEditNotificationAttachmentViewModel requestEditNotificationAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<ResponseGetNotificationAttachmentListViewModel>>
            GetNotificationAttachmentList(RequestGetNotificationAttachmentViewModel requestGetNotificationAttachmentViewModel, int userId);

        Task<IBusinessLogicResult<bool>> DeleteNotificationAttachment(
            int notificationAttachmentId, int userId);
    }
}