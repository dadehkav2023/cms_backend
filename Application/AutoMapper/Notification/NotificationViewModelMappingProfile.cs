using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Attachment.Response;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.Notification.Notification.Response;
using AutoMapper;
using Domain.Entities.Notification;

namespace Application.AutoMapper.Notification
{
    public class NotificationViewModelMappingProfile : Profile
    {
        public NotificationViewModelMappingProfile()
        {
            CreateMap<RequestNewNotificationViewModel, Domain.Entities.Notification.Notification>().ReverseMap();
            CreateMap<RequestEditNotificationViewModel, Domain.Entities.Notification.Notification>().ReverseMap();
            CreateMap<ResponseGetNotificationViewModel, Domain.Entities.Notification.Notification>().ReverseMap();
            
            //Attachment
            CreateMap<RequestNewNotificationAttachmentViewModel, NotificationAttachment>().ReverseMap();
            CreateMap<RequestEditNotificationAttachmentViewModel, NotificationAttachment>().ReverseMap();
            CreateMap<ResponseGetNotificationAttachmentViewModel, NotificationAttachment>().ReverseMap();
        }
    }
}