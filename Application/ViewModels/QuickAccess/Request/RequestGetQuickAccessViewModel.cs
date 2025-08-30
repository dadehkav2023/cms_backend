using Application.ViewModels.Public;

namespace Application.ViewModels.QuickAccess.Request
{
    public class RequestGetQuickAccessViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }
    }
}