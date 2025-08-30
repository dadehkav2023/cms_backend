using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.Notification.Notification.Response
{
    public class ResponseGetNotificationListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetNotificationViewModel> NotificationList { get; set; }
    }

    public class ResponseGetNotificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        
    }
}