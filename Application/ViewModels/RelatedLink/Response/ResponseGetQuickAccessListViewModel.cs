using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.RelatedLink.Response
{
    public class ResponseGetRelatedLinkListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetRelatedLinkViewModel> RelatedLinkList { get; set; }
    }

    public class ResponseGetRelatedLinkViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsActive { get; set; }
    }
}