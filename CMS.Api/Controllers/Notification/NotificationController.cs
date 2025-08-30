using System.Threading.Tasks;
using Application.Services.Notification;
using Application.Services.Notification.Attachment;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.ServiceDesk.Request;
using CMS.Api.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers.Notification
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationAttachmentService _notificationAttachmentService;

        public NotificationController(INotificationService notificationService,
            INotificationAttachmentService notificationAttachmentService)
        {
            _notificationService = notificationService;
            _notificationAttachmentService = notificationAttachmentService;
        }


        //[Authorize]
        [HttpPost("GetNotification")]
        public async Task<IActionResult> GetNotification(
            [FromBody] RequestGetNotificationViewModel requestGetNotificationViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationService.GetNotificationList(requestGetNotificationViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("Attachment/GetAttachment")]
        public async Task<IActionResult> GetNotificationAttachment(
            [FromForm] RequestGetNotificationAttachmentViewModel requestGetNotificationAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationAttachmentService.GetNotificationAttachmentList(
                    requestGetNotificationAttachmentViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

    }
}