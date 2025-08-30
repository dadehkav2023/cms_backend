using Application.ViewModels.Public;

namespace Application.ViewModels.Notification.Notification.Request
{
    public class RequestGetNotificationViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}