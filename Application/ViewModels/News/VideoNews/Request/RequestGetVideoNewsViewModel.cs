using System.Collections.Generic;
using Application.ViewModels.Public;

namespace Application.ViewModels.News.VideoNews.Request
{
    public class RequestGetVideoNewsViewModel : RequestGetListViewModel
    {
        public int? Priority { get; set; }

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool? IsActive { get; set; }
        public bool? ShowInMainPage { get; set; }

        public bool LoadPublishedNews { get; set; }
        
        public List<int>? CategoryIds { get; set; }
        public string StartDateTime { get; set; }        
        public string EndDateTime { get; set; }  
    }
}