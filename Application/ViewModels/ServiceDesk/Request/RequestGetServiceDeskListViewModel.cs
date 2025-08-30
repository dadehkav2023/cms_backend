using Application.ViewModels.Public;

namespace Application.ViewModels.ServiceDesk.Request
{
    public class RequestGetServiceDeskListViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
    }
}