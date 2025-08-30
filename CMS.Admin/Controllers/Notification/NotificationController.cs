using System.Threading.Tasks;
using Application.Services.Notification;
using Application.Services.Notification.Attachment;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.ServiceDesk.Request;
using CMS.Admin.Helper.Response;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers.Notification
{
    [Route("api/admin/[controller]")]
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
        [HttpPost("NewNotification")]
        public async Task<IActionResult> NewNotification(
            [FromForm] RequestNewNotificationViewModel requestNewNotificationViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationService.NewNotification(requestNewNotificationViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("EditNotification")]
        public async Task<IActionResult> EditNotification(
            [FromForm] RequestEditNotificationViewModel requestEditNotificationViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationService.EditNotification(requestEditNotificationViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
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
        [HttpDelete("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(
            [FromForm] int notificationId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationService.DeleteNotification(notificationId, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPost("Attachment/NewAttachment")]
        public async Task<IActionResult> NewNotificationAttachment(
            [FromForm] RequestNewNotificationAttachmentViewModel requestNewNotificationAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationAttachmentService.NewNotificationAttachment(
                    requestNewNotificationAttachmentViewModel, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }

        //[Authorize]
        [HttpPut("Attachment/EditAttachment")]
        public async Task<IActionResult> EditNotificationAttachment(
            [FromForm] RequestEditNotificationAttachmentViewModel requestEditNotificationAttachmentViewModel)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationAttachmentService.EditNotificationAttachment(
                    requestEditNotificationAttachmentViewModel, userId))
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

        //[Authorize]
        [HttpDelete("Attachment/DeleteAttachment")]
        public async Task<IActionResult> DeleteNotificationAttachment(
            [FromForm] int notificationAttachmentId)
        {
            var userId = 1;
            //todo Get UserId
            return (await _notificationAttachmentService.DeleteNotificationAttachment(
                    notificationAttachmentId, userId))
                .ToWebApiResult()
                .ToHttpResponse();
        }
    }
}