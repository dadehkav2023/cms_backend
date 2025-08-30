using Application.ViewModels.Public;

namespace Application.ViewModels.RelatedLink.Request
{
    public class RequestGetRelatedLinkViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }
    }
}